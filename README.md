# BrowserProtect

## What is this ?
BrowserProtect is a C# project which make your **browsers** completly **immune to malwares**. It supports <ins>Edge, Chrome, Brave and OperaGX</ins>

## How does it works ?
When you save your passwords in your browser, they are encrypted, but the key to decrypt them is stored on your computer in a file called "Local State". Many malware programs know this and specifically look for this file to steal the decryption key and access your saved passwords.

BrowserProtect works by modifying your browser to change the name of the "Local State" file. As a result, malware can no longer easily find the key and steal your passwords.

Please note that this project is still in development and may have some bugs or limitations.

## Download
1. Download/clone this depot
2. Compile the source code with a C# compiler like Visual Studio
3. Run it (It requires administrator rights)

## TODO
- Make it more stable to avoid issues
- Make a GUI
