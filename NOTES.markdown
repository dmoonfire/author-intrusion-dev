# Operations

* Replace Token
	* Takes a TokenId and replaces it with a zero or more Tokens

* Operations are submitted to a BufferController
	* When a user submits a change to the system:
		* That item is added to the undo system
		* Immediate actions are performed
			* These may be added to the undo buffer
* Each BufferController has a single Buffer associated with it.
* A Buffer may have multiple controllers.

# General Flow

1. User performs an action, such as ReplaceToken
2. Submits it to the appropriate BufferController
3. Controller adds it into the undo system
4. Controller performs the action
5. Buffer then performs any immediate actions
	a. These are also added to the undo
6. Controller then triggers events on the same thread to update state
7. Controller triggers background processing (if needed)
8. Go back to one and wait for feedback

* Buffer
	* BufferController
		* UserBufferController
		* BackgroundBufferController

On the idle loop

1. Idle system calls the BackgroundBufferController
	a. Gives a maximum execution time

# Events

* TokenChanged(LineKey, Token)
	* Represents a known token as its attributes or tags changed
* TokenRemoved(LineKey, TokenKey, RemovedOptions)
	* Indicates that a token has been removed from the system
	* RemovedOptions determine where the mouse went
* TokenReplace(LineKey, TokenKey, IEnumerable<Token>, ReplacedOptions)
