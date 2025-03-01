# BrowserProtect

## What is this ?
BrowserProtect is a C# project which make your **browsers** completly **immune to malwares**. It supports <ins>Edge, Chrome, Brave and OperaGX</ins>

## How does it works ?
When your passwords are saved on your computer, your browser encrypts them. The decryption key is saved in a json file called "Local State". Malwares are reading this file to find the key and steal your passwords. What does BrowserProtect do is modifying the browser to change the name of the "Local State" file to something else. This makes malwares unable to get the decryption key and steal your passwords. Keep in mind that this project is in development

## Download
1. Download/clone this depot
2. Compile the source code with a C# compiler like Visual Studio
3. Run it
