# Discovering the possibilities of eye gaze cues in on-site collaboration games 

This repository contains the source code of a project developed as part of a bachelor's project at the University of Fribourg, supervised by the Human-IST group.

## Project description: 

This project involves migrating an augmented reality collaborative game from MRTK2 to MRTK3 and creating two versions, one maintaining the original format and another integrating gaze visualizations through MRTK3's eye-tracking functionality.These visualizations are selected based on a literature review, and aim to enhance the user interactions and their efficiency. User tests reveal diverse behavioral patterns, including a prevalent leader-follower strategy. It also shows the way users communicate and resolve the game can be influenced by the presence of gaze visualizations. The study highlights the potential of gaze visualizations to shape collaborative strategies and improve communication dynamics in augmented reality environments.

## Installation requierements: 

- [Unity](https://unity.com/download) (version 2022.3.19f1)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
  
## How to run: 

1. Clone this project into your local directory using the following command:
```bash
git clone [https://github.com/morcant-ui/bachelor_project_morgane_kappeler.git]
```
3. Open Unity Hub and click on the `Add` button. Navigate to your local repository and select the `src` folder of the project. Unity will import the project files, which may take some time. During the import process, accept any dialogues or prompts that appear. There will be errors when the prokect is loaded, this is normal. Refer to the final section for additional setup instructions required to run the project.
4. Open the Unity scene named "Starting Scene".
5. To run the project in the Unity editor, click on the Play button (the arrow in the upper middle part of the window).

### Deploying to Microsoft HoloLens 2:

1. Open Unity Hub and the project as described above.
2. Navigate to `Files -> Build Settings` in Unity.
3. In the Build Settings window:
  - Select `Universal Windows Platform` as the platform.
  - Set Architecture to `ARM64`.
  - Choose `Release` as the Build Configuration.
4. Click on the `Build` button. This will prompt you to create a folder named `Builds` in the `src` directory. Choose this location to build the project.
5. Once the build process completes, navigate to the `Builds` folder and locate the `bachelor_project_morgane_kappeler.sln` file.
6. Open `bachelor_project_morgane_kappeler.sln` with Visual Studio.
7. Configure the deployment settings in Visual Studio following [these instructions](https://learn.microsoft.com/en-us/windows/mixed-reality/develop/advanced-concepts/using-visual-studio?tabs=hl2).
8. In Visual Studio, open the `Package.appxmanifest` file and add the following line inside the `<Capabilities>` section:
```xml
<DeviceCapability Name="broadFileSystemAccess" />
```
9. Build and deploy the application to your HoloLens 2 device by clicking on Start without debugging in Visual Studio.

### Required Changes for the Project to Run

During the migration from MRTK2 to MRTK3, certain issues were overlooked. As of today, this has resulted in problems in some MRTK packages loaded into the project that must be fixed to run the project successfully, whether in the Unity editor or when deploying it to headsets. This section will instruct on the necessary changes to be made.

All code related to the `NonNativeKeyboard` fields must be commented out. This is done in the following files:

- `\src\Library\PackageCache\org.mixedrealitytoolkit.uxcore@18fa67638b66\Experimental\NonNativeKeyboard\TMPInputFieldNonNativeKeyboardTrigger.cs`
- `\src\Library\PackageCache\com.microsoft.mixedreality.toolkit.foundation@f2323a9b7aec\SDK\Experimental\NonNativeKeyboard\Scripts\CapsLockHighlight.cs`
- `\src\Library\PackageCache\org.mixedrealitytoolkit.uxcore@18fa67638b66\Experimental\NonNativeKeyboard\NonNativeValueKey.cs`
- `\src\Library\PackageCache\org.mixedrealitytoolkit.uxcore@18fa67638b66\Experimental\NonNativeKeyboard\NonNativeFunctionKey.cs`
- `\src\Library\PackageCache\com.microsoft.mixedreality.toolkit.foundation@f2323a9b7aec\SDK\Experimental\NonNativeKeyboard\Scripts\KeyboardKeyFunc.cs`
- `\src\Library\PackageCache\com.microsoft.mixedreality.toolkit.foundation@f2323a9b7aec\SDK\Experimental\NonNativeKeyboard\Scripts\SymbolKeyboard.cs`
- `\src\Library\PackageCache\com.microsoft.mixedreality.toolkit.foundation@f2323a9b7aec\SDK\Experimental\NonNativeKeyboard\Scripts\KeyboardValueKey.cs`

