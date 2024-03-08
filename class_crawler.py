import os
import sys
from os import listdir
from os.path import isfile, join

import glob
import pathlib

files = []
result_all_classes = []
path_objects = f"{pathlib.Path(__file__).parent.resolve()}\\Objects\\Objects"
path_converters = f"{pathlib.Path(__file__).parent.resolve()}\\Objects\\Converters"


def get_files_in_path(
    mypath, existing_files, folder_condition=None, file_condition=None
):
    """Recursively get all files in folder and subfolders."""
    all_folders = get_folders_in_path(mypath, [])
    for folder in all_folders:
        if folder_condition is None or folder_condition(folder) is True:

            for f in listdir(folder):
                if isfile(join(folder, f)) and (
                    file_condition is None or file_condition(f) is True
                ):
                    existing_files.append(folder + "\\" + f)
    return existing_files


def get_folders_in_path(mypath, existing_folders):
    """Recursively get all subfolders."""
    all_folders = [
        mypath + "\\" + f for f in listdir(mypath) if not isfile(join(mypath, f))
    ]
    for folder in all_folders:
        if not folder.endswith("bin") and not folder.endswith("obj"):
            existing_folders.append(folder)
            get_folders_in_path(folder, existing_folders)
    return existing_folders


def file_condition_classes(file_name):
    """Condition to select files for extractig class names."""
    if file_name.endswith(".cs"):
        return True
    else:
        return False


files = get_files_in_path(path_objects, [], file_condition=file_condition_classes)


###################################### get classes from files (assuming 1 class per file)
def get_trimmed_strings_per_line_from_files(
    trim_0_start, trim_0_end, trim_1_start, trim_1_end, files
):
    all_classes = []
    for file in files:
        with open(file) as f:
            start_str = None
            end_str = None

            for line in f.readlines():
                if not line.startswith("//") and not line.startswith("      //"):

                    if trim_0_start in line and trim_0_end in line:
                        start_str = line.split(trim_0_start)[1].split(trim_0_end)[0]
                    elif trim_1_start in line and trim_1_end in line:
                        end_str = line.split(trim_1_start)[1].split(trim_1_end)[0]

                    if start_str and end_str:
                        all_classes.append(start_str + "." + end_str)
                        break

    return all_classes


result_all_classes.extend(
    get_trimmed_strings_per_line_from_files(
        "namespace ", ";", "public class ", " :", files
    )
)

for c in result_all_classes:
    print(c)


################################################## get files for conversions
def folder_condition_converter(folder_name):
    if folder_name.endswith("Shared"):
        return True
    else:
        return False


def file_condition_converter(file_name):
    if (
        file_name.endswith(".cs")
        and file_name.startswith("Converter")
        and len(file_name.split(".")) == 2
        and "Utils" not in file_name
    ):
        return True
    else:
        return False


files_conversions = get_files_in_path(
    path_converters, [], folder_condition_converter, file_condition_converter
)

for file in files_conversions:
    print(file)


################################################## get CanConvertToNative function
def get_trimmed_strings_multiline_from_files(trim_start, trim_end, files):
    converters = []
    for file in files:
        print(file)
        with open(file, encoding="utf-8") as f:
            string = ""

            for line in f.readlines():
                if not line.startswith("//") and not line.startswith("      //"):

                    if trim_start in line:  # start writing string
                        string += line
                    elif len(string) > 0 and trim_end in line:
                        string += line.split(trim_end)[0]
                        break
                    elif len(string) > 0:  # keep adding lines
                        string += line
        converters.append(string)
        print(string)

    return converters


# def get_conversion_to_native_bool():
trim_start = "public bool CanConvertToNative("
trim_end = "public bool "
converters = get_trimmed_strings_multiline_from_files(
    trim_start, trim_end, files_conversions
)
