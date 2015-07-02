using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ExitGames.Client.Photon.Chat;
using System.Text;

public class ChatManager : MonoBehaviour,IChatClientListener
{
    /// <summary>
    /// Static reference.
    /// </summary>
    public static ChatManager instance;

	/// <summary>
	/// Gameobject with a feedback for connecting to chat.
	/// </summary>
	[SerializeField]
	GameObject connectingFeedback;

	/// <summary>
	/// Reference to the button to type a message.
	/// </summary>
	[SerializeField]
	Button typeMessageButton;

    /// <summary>
    /// Text input to  the player the message he is writing.
    /// </summary>
    [SerializeField]
    InputField messageInput;

    /// <summary>
    /// Text showing the chat messages log.
    /// </summary>
    [SerializeField]
    Text messagesText;

    /// <summary>
    /// Chat client reference.
    /// </summary>
    ChatClient client;

    /// <summary>
    /// Name of the photon room. Copied to the chat room.
    /// </summary>
    string roomName;

    /// <summary>
    /// Chat messages log.
    /// </summary>
    string messagesList;

	/// <summary>
	/// Boolean containing if chat should be active.
	/// </summary>
	bool chatActive;

    /// <summary>
    /// Initialization method. Just gets the static reference.
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Initializes connection with chat system, entering the room with the same name as the room the player is in.
    /// </summary>
    public void StartConnection()
    {
		chatActive = true;
        this.roomName = MultiplayerRoomsManager.instance.myRoomInfo.name;

        this.client = new ChatClient(this);
        this.client.Connect("90dae08d-093a-4c23-983d-4d950fcc90a2", "1.0", AccountManager.instance.displayName, null);
    }

    /// <summary>
    /// Sends message written to the server, if the input has something to send.
    /// </summary>
    public void SendChatMessage()
    {
        if (messageInput.text.Length > 0)
        {
            Debug.Log("send message " + messageInput.text);
            client.PublishMessage(roomName, messageInput.text);
            messageInput.text = "";
        }
    }

    /// <summary>
    /// Update method. Needed to make the chat work.
    /// </summary>
    void Update()
    {
        if (this.client != null)
        {
			if (this.client.State == ChatState.Disconnected && chatActive)
			{
				this.StartConnection();
			}
            this.client.Service();
        }
    }

    /// <summary>
    /// Leaves chat room. Used when player leaves room, or enters game.
    /// </summary>
    public void LeaveChat()
    {
		chatActive = false;
        this.client.Disconnect();
    }

    /// <summary>
    /// Handles application exit. Leaves chat.
    /// </summary>
    public void OnApplicationQuit()
    {
        if (this.client != null)
        {
            this.LeaveChat();
        }
    }

    /// <summary>
    /// On Disconnected event.
    /// </summary>
    public void OnDisconnected()
    {

    }
    /// <summary>
    /// On Connected event.
    /// </summary>
    public void OnConnected()
    {
        this.client.Subscribe(new string[] { roomName });
    }
    public void OnChatStateChange(ChatState state)
    {
        
    }
    /// <summary>
    /// On get messages event. Called to get the chat log, when the player enters the room.
    /// </summary>
    /// <param name="channelName">Name of the channel he has substribed to.</param>
    /// <param name="senders">List of senders of the sent messages.</param>
    /// <param name="messages">List of sent messages.</param>
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < messages.Length; i++)
        {
            builder.AppendLine(senders[i]+":");
            builder.AppendLine(messages[i].ToString());
            builder.AppendLine("");
        }

        messagesList += builder.ToString();
        messagesText.text = messagesList;
    }
    /// <summary>
    /// On private message event. Calles when the player receives a private message. Not used for this game.
    /// </summary>
    /// <param name="sender">Sender name.</param>
    /// <param name="message">Message sent.</param>
    /// <param name="channelName">Channel name.</param>
    public void OnPrivateMessage(string sender, object message, string channelName)
    {

    }
    /// <summary>
    /// On subscribed event. Called when the player receives some result of channel subscription.
    /// </summary>
    /// <param name="channels">Channels names.</param>
    /// <param name="results">Results of the subscription attempts.</param>
    public void OnSubscribed(string[] channels, bool[] results)
    {
        ChatChannel channelInfo = client.PublicChannels[roomName];

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < channelInfo.MessageCount; i++)
        {
            builder.AppendLine(channelInfo.Senders[i] + ":");
            builder.AppendLine(channelInfo.Messages[i].ToString());
            builder.AppendLine("");
        }

        messagesList += builder.ToString();
        messagesText.text = messagesList;

		connectingFeedback.SetActive(false);
		messageInput.gameObject.SetActive(true);
		typeMessageButton.gameObject.SetActive(true);
    }
    /// <summary>
    /// On unsubscribed event. Called when the player unsubscribes of a channel. Not used for this game.
    /// </summary>
    /// <param name="channels">Channels unsubscribed.</param>
    public void OnUnsubscribed(string[] channels)
    {

    }
    /// <summary>
    /// On status update event. Called when some player changes their status. Not used for this game.
    /// </summary>
    /// <param name="user">User name.</param>
    /// <param name="status">Status ID.</param>
    /// <param name="gotMessage"></param>
    /// <param name="message">Message.</param>
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }
}