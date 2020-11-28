use std::env;
use std::fs;
use std::vec::Vec;
use std::path::Path;
use std::fs::read_dir;

fn main() {
    let out_dir = env::var_os("OUT_DIR").unwrap();
    let dest_path = Path::new(&out_dir).join("enum.g.rs");
    let mut lines : Vec<String> = Vec::new();

    let path = Path::new("../definitions.d/Enums");
    if path.exists() && path.is_dir()
    {
        let dir_content = path.read_dir().unwrap();
        for entry in dir_content
        {
            let entry_path = entry.unwrap().path();
            if !entry_path.is_dir()
            {
                continue;
            }

            let name = entry_path.file_name().unwrap().to_str().unwrap();
            let line = format!("pub enum {}", name);
            lines.push(line.to_string());
            lines.push("{".to_string());

            let enum_files = read_dir(entry_path).unwrap();
            for enum_value in enum_files
            {
                let enum_file_path = enum_value.unwrap().path();
                if enum_file_path.is_dir()
                {
                    continue;
                }

                let value = enum_file_path.file_stem().unwrap().to_str().unwrap();
                let name = enum_file_path.extension().unwrap().to_str().unwrap();
                let comment = fs::read_to_string(enum_file_path.to_str().unwrap()).unwrap();

                lines.push(format!("    {} = {}, // {}", name, value, comment));
            }

            lines.push("}".to_string());
        }
    }
    else
    {
        panic! ("No files found at {}", path.display());
    }

    let content = lines.join("\n");

    fs::write(
        &dest_path,
        content
    ).unwrap();
    println!("cargo:rerun-if-changed=build.rs");
}