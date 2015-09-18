# Unity-Drag-and-Drop

Based on the tutorial found at [quil18.com](http://quill18.com/unity_tutorials/).

It was done in Unity 5.2.

**Differences**
* Shows a drop-indicator where the card will be inserted
* Rewrote the logic. The drop-zones are kept dumb.
* Reusing the same place-holder instead of creating / destroying it.



**Known issue**:

Selecting the 2nd card and slightly dragging it left - will make the placeholder be shown as the first element. 
It should have waited until the dragged card was moved more to the left.
