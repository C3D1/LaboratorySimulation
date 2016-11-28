How to add an existing project:

1) Download github
2) Start Github GUI
3) If you haven't downloaded the project yet, click the cross in the upper left and choose clone.



How to get the latest version of a project:

via GUI:
1) Open Github
2) Click the sync-button in the upper right.

via Git Bash:
If you doesnt care about local changes ->
1) change to your directory -> cd:[directory where you've saved your github project]
2) if you aren't on correct branch you can change your branch with git checkout [your branch]
3) git reset --hard
4) git clean -f
5) git pull

Check online which was the latest commit in this project and compare it with your local one. If it's the same, everything worked fine.



Unity settings:

1) Start Unity
2) Build Project File -> Build Settings
3) Scenes in build should be in this order: 
   _Scenes/Login 0
   -Scenes/Station_T_Current 1
4) Click on Build and choose the directory, where already an instance exists.For example LaboratorySimulation\Instances\Oculus_Rift.
4.1) Build the lightning when you click on the lightning-tab next to the hierarchy and click on the button below named build.


Date 28.11.2016