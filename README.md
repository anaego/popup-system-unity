# popup-system-unity
A test task to create a dynamic popup system

- A short text document describing the development process
	- Important decisions you took and why
		- To use unirx or some kind opf message system, same with zenject (opted for simplicity)
		- How to organize architecture
		- Optimizatiuon: object pool, how to avoid instantiating a destroying a lot
	- Describe the problem you had to face
		- Where should url be a setting - in an SO or in editor in the component? (In the end, allowed setting it in the test panel itself)
		- Dynamic asset loading from URL - how to make it work n async variant
	- Elaborate what you would do if you had more time
		- Image loading - choosing custom sprite on the test panel
		- More animation settings
		- Disabled and active states of buttons
		- Queue limit that does something sensible when that limit is reached
		- Some alternative to singleton for queue
		- Zenject, unirx, more patterns, but I'd have to think ab too much optimization also to reach a balance of time and result
		- For test panel - texts to setting because of potential localization
		- Messenger system instead of callbacks
		- Explicit setting on the test panel for if we should wait for the popup button click or fade anyway
		- Interfaces for pool, poolable and queue & in general make them more reusable
		- Refactor some big methods
		- Refactor ButtonActionData to be more versatile and changeable
		- Refactor PopupData to make it serializable
