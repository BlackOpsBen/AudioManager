# AudioManager
An easy to use, extensible audio manager for easily adding sounds to your games.

[Watch a video tutorial](https://youtu.be/QQjDGkTsBAA)

This repo has a demo scene, but all you need is to extract the 3 scripts and put them in your projects asset folder. The 3 scripts are:
- AudioManager.cs
- RemoveSourceOnDestroy.cs
- SoundCue

To get started follow these steps:
1. Create an empty GameObject in your scene.
2. Attach the "AudioManager" component.
3. Create a "Resources" folder inside your "Assets" folder, if you don't have one already.
4. Put any and all sound files / audio clips that you want to use in the Resources folder. Sub-folder structure can be whatever you want.
5. To play one of these clips, type "AudioManager.Instance.PlayAudioClip" to see a list of different ways you can play an audio clip. Provide the required arguments to the functions.

# SoundCues
Advanced way to play sounds.
Create groups of clip options to randomly play and modulate the pitch and volume each time.

To create and play SoundCues:
1. Right click in the project window and select "Audio/SoundCue" to create a SoundCue ScriptableObject.
2. Add as many audio clips as you wish to the "Clip Options" list.
3. Disable or enable modulation and set the values to your liking.
4. To play the SoundCue, type "AudioManager.Instance.PlaySoundCue"
