# :gem: Activity Launcher for Windows :gem: 

## TL;DR
Enables users to create personalized activity sets for quick and efficient task automation, streamlining various processes like launching software, opening files, and more.

## Goal and motivation :fire:
Activity Launcher for Windows is an intuitive desktop application designed to simplify task automation and enhance productivity. With this software, users can create personalized "Activities" by grouping a series of predefined tasks together. Whether it's launching essential software, opening file directories, or loading specific web pages, "ActivityLauncher" streamlines the process, saving time and effort. The user-friendly interface allows easy management of activities, customization of tasks, and quick execution with just a few clicks. It aims to optimize workflow and provide a seamless experience for users looking to streamline their computer-based activities. The application will be available on Windows as a desktop application and will be open source and free.

The idea behind it is to make it easier to switch between activities on your computer. As a software engineer, we can sometime switch between totally different code base and need to reopen lots of different applications and tools depending on the project. This tool will be made to help you achieve that with hopefully less pain.

## Technology
The application would be developed using the [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui) Blazor hybrid platform and would be available only on Windows. The UI and UX will be based on the [MudBlazor component library](https://mudblazor.com).

## Project
This is a small project to try a first app using [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui) and is not intended to be really complex nor long term maintained. The project is open source and free to use, feel free to download and use it or to fork it and develop your own features. Every feedback or suggestions of change or feature would be appreciated, feel free to contact me by mail [abouaban2@gmail.com](mailto:abouaban2@gmail.com).

### Behind the scene
To allow more flexibility, every activity will compile to a PowerShell script that will be responsible for executing the tasks, it will be invoked when the user launch an activity or at startup. This way, a user can use this app only as a startup script generator and fully customized them at will.

Activities will be saved as JSON files and will be loaded when launching the application. Making it also available for customization if needed (at your own risks).

## Todo

### Setup
- [x] Setup project with MAUI and Blazor
- [x] Add Mud Blazor to the project

### Minimum Viable Product
- [x] Start a hard-coded activity and make it work
- [x] Persist and load the activities
- [x] Create an activity
- [x] Edit and activity
- [ ] Manage activity tasks
- [ ] Show tasks in activities
- [ ] Allow the user to set up the activity to launch at startup

### Nice to have
- [ ] Publish the application to the public
- [ ] Build the application on new git tag and publish it to GitHub to make it available for download
- [ ] Create a way for user to have their own custom tasks without having to use PowerShell scripting


<sub>Some of the texts in this project are generated using [Chat GPT](https://openai.com/blog/chatgpt).</sub>
