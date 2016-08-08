________________________________________________________________________________________
                                     Automatic LOD
                          Copyright © 2015 Ultimate Game Tools
                            http://www.ultimategametools.com
                               info@ultimategametools.com

                                         Twitter (@ugtools): https://twitter.com/ugtools
                                    Facebook: https://www.facebook.com/ultimategametools
                               Google+:https://plus.google.com/u/0/117571468436669332816
                                 Youtube: https://www.youtube.com/user/UltimateGameTools
________________________________________________________________________________________
Version 1.01


________________________________________________________________________________________
Introduction

Automatic LOD is a powerful Unity extension that allows you to quickly generate and
manage multiple levels of detail for your 3D models.
LOD levels are simplified meshes that are generated procedurally by the extension and
help optimize your game especially on lower end platforms.
The Automatic LOD component takes care at runtime of enabling the correct mesh for each
object depending on its distance to the camera or its screen area covered.
This way you can use meshes with lower polycount when the object is far away or covers
small screen space and higher polycounts for up-close views.
Use detail only when you need it!

Features:
-Generate LOD level meshes fully procedurally with just one click
-Supports both static and skinned meshes
-LODs are automatically handled by the component. No scripting needed
-Build on top of our Mesh Simplify extension which is also included
-Includes full source code
-Includes high quality 3D models and sample scenes seen on the screenshots
-Clean, easy to use and powerful UI with multiediting support
-Valid for all platforms! Especially useful on mobile
-Supports complex object hierarchies with sub-objects and multiple materials
-Live preview both in edit mode and play mode
-Supports both screen coverage and distance to camera algorithms
-Allows finetuning assigning priorities to vertices using volumes
-Classic mesh decimation/polygon count reduction using the included Mesh Simplify tool


________________________________________________________________________________________
Requirements

Unity 5.1.1 or above


________________________________________________________________________________________
Help

For up to date help: http://www.ultimategametools.com/products/automatic_lod/help
For additional support contact us at http://www.ultimategametools.com/contact


________________________________________________________________________________________
Acknowledgements

-3D Models especially developed by:
    Simon Remis (http://http://www.simonremis.com)
    Luis Santander (http://www.luissantanderart.com)
    Matías Baena (http://matiasbaena.wordpress.com)

	 
________________________________________________________________________________________
Version history

V1.01 - 16/08/2015:

[FIX] Object-camera distance is now computed using the renderer bounding box instead
      of the object's pivot. This gives better results in objects that have the pivot
	  not placed properly.

[FIX] LOD child nodes now switch between the same LODs as the parent root even if
      the "Override xxx settings" has been activated. This gives smoother results.

[FIX] Sample scene #2 now has Torii sharing the same LOD settings. Previous versions
      had different settings which could create confusion as LODs would pop at
	  different distances and different resolutions.
	  
V1.00 - 29/07/2015:

[---] Initial release
