
To create a new view:
 1. Drag the "View" and "Switch" prefabs into the scene and scale/reposition as desired.
 2. On the Switch object, configure the AdvanceMap script with the 2 views it will swap between.

When travelling to a different part of the map, the camera will only render what's behind the white sprite.

To hide the white sprites in scene view, disable them from the inspector, then disable the Light Image Effect script on the Main Camera.


---------------------------------------------------------------------------------------------------------------------------------------------

To create a sprite that scales on one side:
 1. In the sprite inport settings, set the pivot to point you want to scale it from (eg, to scale up on the Y axis pivot should be at the Bottom, to scale from the left and top, pivot should be in the bottom right corner).
 2. Setup the Sprite as a View (drag it into inspector and set the Layer to Views). Set the View to the max desired scale.
 3. Setup a Wheel from the Prefabs folder. In the Scale From Side script, choose which axis you want to scale on, and enter a scale value (positive values make the sprite bigger, negatives make it smaller). Ensure the object is on the Interactible Layer.
 4. In game, press E to interact with the wheel and Z to scale down the view and X to scale up.

 In order to move boxes using the view colliders, make sure they have a collider 2D and a dynamic rigidbody 2D attached. (Check Box prefab for example)
