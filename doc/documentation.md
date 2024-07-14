# Documentation of an MRTK3 project with Unity

This document has the purpose of presenting the challenges that arose while in the process of migrating a project from MRTK2 to MRTK3 and including eye-tracking in it. 

## Migration

### What is is about?

The migration guide from MRTK2 to MRTK3 outlines significant changes and improvements across interactions, UX components, and input configurations. MRTK3 introduces the XR Interaction Toolkit (XRI), utilizing Unity's XR Input System and OpenXR for enhanced interaction handling. Key terminologies have been updated: Pointers are now Interactors, and objects interacted with are Interactables. The Locomotion System has evolved to include Snap Turn and Continuous Move providers. Additionally, the XR Interaction Manager and Interaction Mode Manager offer enhanced flexibility. UX components in MRTK3 focus on both Canvas and Non-Canvas UI elements, with ongoing updates and additions. Input configurations leverage the new Unity Input System Package, allowing for streamlined input actions and gestures, including improved support for speech commands and controller configurations. Overall, MRTK3 simplifies development while expanding functionality for mixed reality applications.
More details can be found [here](https://learn.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk3-overview/architecture/mrtk-v2-to-v3).

### Issues encoutered

Most of the issues encountered were due to the developer's inexperience with Unity. The migration did not pose as much problems. We provide a list of the troubles encoutered solemny because of it here:

- During the migration from MRTK2 to MRTK3, certain issues were overlooked. As of today, this has resulted in problems in some MRTK packages loaded into the project that must be fixed to run the project successfully, whether in the Unity editor or when deploying it to headsets. This section will instruct on the necessary changes to be made. All code related to the `NonNativeKeyboard` fields must be commented out. This is done in the following files:
   - `\src\Library\PackageCache\org.mixedrealitytoolkit.uxcore@18fa67638b66\Experimental\NonNativeKeyboard\TMPInputFieldNonNativeKeyboardTrigger.cs`
   - `\src\Library\PackageCache\com.microsoft.mixedreality.toolkit.foundation@f2323a9b7aec\SDK\Experimental\NonNativeKeyboard\Scripts\CapsLockHighlight.cs`
   - `\src\Library\PackageCache\org.mixedrealitytoolkit.uxcore@18fa67638b66\Experimental\NonNativeKeyboard\NonNativeValueKey.cs`
   - `\src\Library\PackageCache\org.mixedrealitytoolkit.uxcore@18fa67638b66\Experimental\NonNativeKeyboard\NonNativeFunctionKey.cs`
   - `\src\Library\PackageCache\com.microsoft.mixedreality.toolkit.foundation@f2323a9b7aec\SDK\Experimental\NonNativeKeyboard\Scripts\KeyboardKeyFunc.cs`
   - `\src\Library\PackageCache\com.microsoft.mixedreality.toolkit.foundation@f2323a9b7aec\SDK\Experimental\NonNativeKeyboard\Scripts\SymbolKeyboard.cs`
   - `\src\Library\PackageCache\com.microsoft.mixedreality.toolkit.foundation@f2323a9b7aec\SDK\Experimental\NonNativeKeyboard\Scripts\KeyboardValueKey.cs`
- The MultiUserCapabilities package required for PUN integration has not been updated for MRTK3 compatibility. Therefore, it's necessary to use the MRTK2 versions instead. Additionally, certain other packages from MRTK2 must be added to the project to prevent errors. Detailed instructions for this process are outlined [here](https://learn.microsoft.com/en-us/answers/questions/1189757/meet-error-cs0246-the-type-or-namespace-name-ancho).
- All events are now directly linked to MRTK events within the Unity Editor, rather than being directly coded in scripts as was the case with MRTK2. There are several MRTK events available, and developers need to experiment with them to determine which one best suits their specific situation.

We recommend starting with a blank project when migrating from MRTK2 to MRTK3. This approach allows changes to be implemented directly without the need to clean up remnants from the old version, thus minimizing confusion and ensuring a smoother migration process. If the developer is a beginner with Unity and MRTK, we strongly recommend learning the basics before proceeding with any project work.

### Useful ressources:

- [Introduction to Mixed Reality](https://learn.microsoft.com/en-us/training/modules/intro-to-mixed-reality/1-introduction)
- [Get started with Mixed Reality](https://learn.microsoft.com/en-us/windows/mixed-reality/discover/get-started-with-mr)
- [What is Mixed Reality?](https://learn.microsoft.com/en-us/windows/mixed-reality/discover/mixed-reality)
- [HoloLens 2 fundamentals: develop mixed reality applications](https://learn.microsoft.com/en-us/training/paths/beginner-hololens-2-tutorials/)
- [Videos about MRTK](https://www.youtube.com/playlist?list=PLQMQNmwN3FvzWQ1Hyb4XRnVncvCmcU8YY)
- [Example of a migration from MRTK2 to MRTK3](https://localjoost.github.io/MRTK2-to-MRTK3-migrating-the-QRCode-sample/)

### Troubleshooting Strange Issues:

During the development of this application, we encountered some unusual errors that required unconventional fixes. Despite their inexplicable nature, documenting them here may assist in similar situations:

- Loading MRTK2 Project: Initially, the MRTK2 project that we wished to redo in MRTK3 failed to load properly. To resolve this issue, we repeatedly imported and deleted the project in Unity until it opened successfully.
- Package Introduction Bugs: Occasionally, after introducing a new package, a persistent bug would appear. Following the error code did not resolve the issue. Instead, we found that removing the package entirely and then reintroducing it allowed the project to function correctly again.
  
## Eye-tracking

The goal of this section is simply to provide some tutorials that can help to understand the process of adding eyes-tracking into an MRTK3 project. More tutorials can be found on [Microsoft Learn](https://learn.microsoft.com/en-us/windows/mixed-reality/design/eye-tracking).

#### Tutorial inspirations:
- [Enable eye tracking and voice commands for objects on the HoloLens 2](https://learn.microsoft.com/en-us/training/modules/use-eye-tracking-voice-commands/6-3-exercise-eye-tracking)
- [MRTK3 StatefulInteractable gaze, hover and select events - and how to use them ](https://localjoost.github.io/MRTK3-StatefulInteractable-gaze,-hover-and-select-events-and-how-to-use-them/)
- [Accessing and recording eye tracking data with MRTK3 ](https://localjoost.github.io/Accessing-and-recording-eye-tracking-data-with-MRTK3//)
- [Control UI & 3D Objects With Eye Tracking (MRTK 3 Gaze Interactors) ](https://www.youtube.com/watch?v=gWFOw_yb9vY)
- [Accessing and recording eye tracking data with MRTK3](https://localjoost.github.io/Accessing-and-recording-eye-tracking-data-with-MRTK3/)
