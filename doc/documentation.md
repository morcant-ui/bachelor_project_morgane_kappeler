# Documentation of an MRTK3 project with Unity
This document has the purpose of presenting the challenges that arose in the process of migrating a project from MRTK2 to MRTK3 and including eye-tracking in it. 

## Requirements  

## Migration

not started

## Eye-tracking

The goal of this section is to present the process of adding eyes-tracking into an MRTK3 project.

### Creation of a simple application with eye-tracking 

<span style="color:yellow"> **NOTE:** take inspiration from the fundamental of MRTK tutos from Microsoft for the re-redaction of this !!! </span>.

#### Tutorial

1. Create a simple scene in Unity (delete main camera, add MRTK XR Rig and MRTK Input Simulator)
2. Add a 3D Object cube to the hierarchy, name it `cube`
3. 


#### Results
Click [here](https://drive.google.com/file/d/1gj9TqAYwyKLSlId5dA5nQem9XaWhd1pa/view) to see the results.

My observations are  :

- Eye-tracking seems really precise
- 

#### Tutorial inspirations :
- [Enable eye tracking and voice commands for objects on the HoloLens 2](https://learn.microsoft.com/en-us/training/modules/use-eye-tracking-voice-commands/6-3-exercise-eye-tracking)
- [MRTK3 StatefulInteractable gaze, hover and select events - and how to use them ](https://localjoost.github.io/MRTK3-StatefulInteractable-gaze,-hover-and-select-events-and-how-to-use-them/)
- [Accessing and recording eye tracking data with MRTK3 ](https://localjoost.github.io/Accessing-and-recording-eye-tracking-data-with-MRTK3//)
- [Control UI & 3D Objects With Eye Tracking (MRTK 3 Gaze Interactors) ](https://www.youtube.com/watch?v=gWFOw_yb9vY)


## Adding eyes-tracking into the project

### Eye Pointer

EyePointer.cs + prefab Visualizer

#### Tutorial inspirations :
- [Accessing and recording eye tracking data with MRTK3](https://localjoost.github.io/Accessing-and-recording-eye-tracking-data-with-MRTK3/)