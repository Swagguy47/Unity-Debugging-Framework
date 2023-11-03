# Unity-Debugging-Framework
An extendable, ScriptableObject-based dev menu framework that removes itself on build.

Example usage:
![image](https://github.com/Swagguy47/Unity-Debugging-Framework/assets/67041649/aadf419c-99c1-4060-b11b-d08c8b9e083a)


## Supported element types:
- Button
- Slider / float field
- Sub menu

## Useful features:
- Subpages (multiple can be open at once)
- Draggable pages
- Arrow key & cursor-based interaction methods
- Dynamically create page content & events on open (ex: auto-populating item page with all items in game)
- UnityEvent-based functionality

## How to setup:
Fix any errors upon initial import (obviously)
Place debugMenu prefab (or create one yourself) in your UI canvas
Setup all pages as you wish, (default homepage under: `Assets/debugFramework/DevPages/`)
Setup dev events on debugMenu gameobject to correlate with your pages

# ScriptableObject parameters:
## Page Content:
"**Page Name**" = Simple text that is displayed at the header of the page/subpage upon opening

"**Open Event**" = (optional) Triggers the `debugMenu` event of the index provided. Useful for automatically generating page content. -1 ignores 

"**Elements**" = Array of all page elements

## Element Content:
"**Text**" = Text that is displayed on the element's label

"**Event Index**" = Which `debugMenu` event to trigger (based on debugMenu event array index). (ignored if ElementType = Subpage)

"**Event Variable**" = (optional) Passes a float variable into the debugMenu event as an input. Useful for reusing the same event but changing the action, such as giving the player an item.

"**Subpage**" = (only used if ElementType = SubPage) which page to open upon interaction.

"**Element Type**" = Describes what type the element will be represented as, between plain button, float input/slider, & subpage button (opens another menu)
