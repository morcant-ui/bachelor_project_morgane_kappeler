# Documentation of an MRTK3 project with Unity
This document has the purpose of presenting the challenges that arose in the process of migrating a project from MRTK2 to MRTK3 and including eye-tracking in it. 

## Requirements  

## Migration
First steps, as usual :
1. Remove MainCamera
2. Add MRTK XR Rig & MRTK Input Simulator

### Task 1
#### Game Area 
This is the cube that contains the experiment

1. I added a cube (GameObject - 3D Object) to the scene
2. I set the position to 0-1.6-4 and scale to 2-2-2 (<span style="color:yellow"> **NOTE:** compare with what Sophie did  !!! </span>)
3. I changed the material so that it became transparent :
   1. use material of previous project: `Transparent Material` 
   2. put it in `MeshRenderer > Material`
   3. chose shader ``
4. Add Script `SpawnSphere`
   1. spawn sphere in cube
   2. spawn 3 spheres
   3. spawn sphere with diff colors


### TODO

- add le fait de pouvoir bouger le cube au début (setArea, sans sphere inside)
- regarde comme Sophie a fait pr :
  - les positions de départ 
  - enregistrer les données (sphères pop, quelle sphère, couleur ect)
  - les sizes,...
  - ET EN GROS REPRENDRE TT CA PCQ LE BUT C'EST PAS QUE JE REFASSE TOUT !!!!  

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

### Eye Pointer Always Observing

EyePointer.cs + prefab Visualizer

### Eye Pointer Disappearing

Pointer.cs + prefab Visualizer

#### Tutorial inspirations :
- [Accessing and recording eye tracking data with MRTK3](https://localjoost.github.io/Accessing-and-recording-eye-tracking-data-with-MRTK3/)

### TODO

- regarder comment ça marche le eyes-tracking déjà implémenter dans mrtk !!!!