# TelerikFontFinder

## Overview
TelerikFontFinder is a simple C# console application designed to search for specific fonts within `.trdp` files. It scans a given directory for `.trdp` files, extracts them, and searches for occurrences of the specified font within the `definition.xml` files contained in the `.trdp` archives.

## Features
- Search for a specified font in all `.trdp` files within a given directory.
- Extract `.trdp` files and parse `definition.xml` for font occurrences.
- Provide a detailed report of where each instance of the specified font is found.

## Prerequisites
- .NET Core Runtime or SDK installed on your machine.
- Basic knowledge of running console applications.

## Installation
Clone the repository to your local machine using: git clone https://github.com/utesch/TelerikFontFinder.git

## Usage
1. **Run the Application:**
   Navigate to the application's directory and run the application using the .NET CLI: `dotnet run`

2. **Enter the Source Directory:**
When prompted, enter the full path of the directory you wish to search.

3. **Enter the Font Name:**
Next, enter the name of the font you are searching for.

The application will then process each `.trdp` file in the directory and provide a report on the console.

## How It Works
- The application prompts the user for a directory path and a font name.
- It searches the given directory for `.trdp` files.
- Each `.trdp` file is extracted to a temporary directory.
- The application looks for `definition.xml` within the extracted contents and searches for the specified font.
- After processing, a cleanup routine removes the extracted files.

## Limitations
- The application currently only supports `.trdp` file format.
- Large `.trdp` files may impact performance due to the extraction process.

## Contributing
Contributions to the TelerikFontFinder are welcome. Please ensure that your code adheres to the existing style for consistency.

## License
Apache 2.0