using System;
using System.Collections.Generic;
using PlayFab.Internal;
using PlayFab.Serialization.JsonFx;

namespace PlayFab.Server
{
	
	
	
	public class AddCharacterVirtualCurrencyRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// PlayFab unique identifier of the user whose virtual currency balance is to be incremented.
		/// </summary>
		
		public string PlayFabId { get; set;}
		
		
		public string CharacterId { get; set;}
		
		/// <summary>
		/// Name of the virtual currency which is to be incremented.
		/// </summary>
		
		public string VirtualCurrency { get; set;}
		
		/// <summary>
		/// Amount to be added to the user balance of the specified virtual currency.
		/// </summary>
		
		public int Amount { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("VirtualCurrency", VirtualCurrency);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Amount", Amount);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			VirtualCurrency = (string)JsonUtil.Get<string>(json, "VirtualCurrency");
			Amount = (int)JsonUtil.Get<double>(json, "Amount");
		}
	}
	
	
	
	public class AddSharedGroupMembersRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the shared group.
		/// </summary>
		
		public string SharedGroupId { get; set;}
		
		
		public List<string> PlayFabIds { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("SharedGroupId", SharedGroupId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("PlayFabIds", PlayFabIds);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			SharedGroupId = (string)JsonUtil.Get<string>(json, "SharedGroupId");
			PlayFabIds = JsonUtil.GetList<string>(json, "PlayFabIds");
		}
	}
	
	
	
	public class AddSharedGroupMembersResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class AddUserVirtualCurrencyRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// PlayFab unique identifier of the user whose virtual currency balance is to be increased.
		/// </summary>
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Name of the virtual currency which is to be incremented.
		/// </summary>
		
		public string VirtualCurrency { get; set;}
		
		/// <summary>
		/// Amount to be added to the user balance of the specified virtual currency.
		/// </summary>
		
		public int Amount { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("VirtualCurrency", VirtualCurrency);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Amount", Amount);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			VirtualCurrency = (string)JsonUtil.Get<string>(json, "VirtualCurrency");
			Amount = (int)JsonUtil.Get<double>(json, "Amount");
		}
	}
	
	
	
	public class AuthenticateSessionTicketRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Session ticket as issued by a PlayFab client login API.
		/// </summary>
		
		public string SessionTicket { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("SessionTicket", SessionTicket);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			SessionTicket = (string)JsonUtil.Get<string>(json, "SessionTicket");
		}
	}
	
	
	
	public class AuthenticateSessionTicketResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Account info for the user whose session ticket was supplied.
		/// </summary>
		
		public UserAccountInfo UserInfo { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("UserInfo", UserInfo);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			UserInfo = JsonUtil.GetObject<UserAccountInfo>(json, "UserInfo");
		}
	}
	
	
	
	public class AwardSteamAchievementItem : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Unique Steam achievement name.
		/// </summary>
		
		public string AchievementName { get; set;}
		
		/// <summary>
		/// Result of the award attempt (only valid on response, not on request).
		/// </summary>
		
		public bool Result { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("AchievementName", AchievementName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Result", Result);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			AchievementName = (string)JsonUtil.Get<string>(json, "AchievementName");
			Result = (bool)JsonUtil.Get<bool>(json, "Result");
		}
	}
	
	
	
	public class AwardSteamAchievementRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Array of achievements to grant and the users to whom they are to be granted.
		/// </summary>
		
		public List<AwardSteamAchievementItem> Achievements { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Achievements", Achievements);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Achievements = JsonUtil.GetObjectList<AwardSteamAchievementItem>(json, "Achievements");
		}
	}
	
	
	
	public class AwardSteamAchievementResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Array of achievements granted.
		/// </summary>
		
		public List<AwardSteamAchievementItem> AchievementResults { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("AchievementResults", AchievementResults);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			AchievementResults = JsonUtil.GetObjectList<AwardSteamAchievementItem>(json, "AchievementResults");
		}
	}
	
	
	
	/// <summary>
	/// A purchasable item from the item catalog
	/// </summary>
	public class CatalogItem : PlayFabModelBase
	{
		
		
		/// <summary>
		/// unique identifier for this item
		/// </summary>
		
		public string ItemId { get; set;}
		
		/// <summary>
		/// class to which the item belongs
		/// </summary>
		
		public string ItemClass { get; set;}
		
		/// <summary>
		/// catalog item for this item
		/// </summary>
		
		public string CatalogVersion { get; set;}
		
		/// <summary>
		/// text name for the item, to show in-game
		/// </summary>
		
		public string DisplayName { get; set;}
		
		/// <summary>
		/// text description of item, to show in-game
		/// </summary>
		
		public string Description { get; set;}
		
		/// <summary>
		/// price of this item in virtual currencies and "RM" (the base Real Money purchase price, in USD pennies)
		/// </summary>
		
		public Dictionary<string,uint> VirtualCurrencyPrices { get; set;}
		
		/// <summary>
		/// override prices for this item for specific currencies
		/// </summary>
		
		public Dictionary<string,uint> RealCurrencyPrices { get; set;}
		
		/// <summary>
		/// list of item tags
		/// </summary>
		
		public List<string> Tags { get; set;}
		
		/// <summary>
		/// game specific custom data
		/// </summary>
		
		public string CustomData { get; set;}
		
		/// <summary>
		/// array of ItemId values which are evaluated when any item is added to the player inventory - if all items in this array are present, the this item will also be added to the player inventory
		/// </summary>
		
		public List<string> GrantedIfPlayerHas { get; set;}
		
		/// <summary>
		/// defines the consumable properties (number of uses, timeout) for the item
		/// </summary>
		
		public CatalogItemConsumableInfo Consumable { get; set;}
		
		/// <summary>
		/// defines the container properties for the item - what items it contains, including random drop tables and virtual currencies, and what item (if any) is required to open it via the UnlockContainerItem API
		/// </summary>
		
		public CatalogItemContainerInfo Container { get; set;}
		
		/// <summary>
		/// defines the bundle properties for the item - bundles are items which contain other items, including random drop tables and virtual currencies
		/// </summary>
		
		public CatalogItemBundleInfo Bundle { get; set;}
		
		/// <summary>
		/// if true, then this item instance can be used to grant a character to a user.
		/// </summary>
		
		public bool CanBecomeCharacter { get; set;}
		
		/// <summary>
		/// if true, then only one item instance of this type will exist and its remaininguses will be incremented instead
		/// </summary>
		
		public bool IsStackable { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("ItemId", ItemId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemClass", ItemClass);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CatalogVersion", CatalogVersion);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("DisplayName", DisplayName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Description", Description);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("VirtualCurrencyPrices", VirtualCurrencyPrices);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("RealCurrencyPrices", RealCurrencyPrices);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Tags", Tags);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CustomData", CustomData);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("GrantedIfPlayerHas", GrantedIfPlayerHas);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Consumable", Consumable);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Container", Container);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Bundle", Bundle);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CanBecomeCharacter", CanBecomeCharacter);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("IsStackable", IsStackable);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			ItemId = (string)JsonUtil.Get<string>(json, "ItemId");
			ItemClass = (string)JsonUtil.Get<string>(json, "ItemClass");
			CatalogVersion = (string)JsonUtil.Get<string>(json, "CatalogVersion");
			DisplayName = (string)JsonUtil.Get<string>(json, "DisplayName");
			Description = (string)JsonUtil.Get<string>(json, "Description");
			VirtualCurrencyPrices = JsonUtil.GetDictionaryUInt32(json, "VirtualCurrencyPrices");
			RealCurrencyPrices = JsonUtil.GetDictionaryUInt32(json, "RealCurrencyPrices");
			Tags = JsonUtil.GetList<string>(json, "Tags");
			CustomData = (string)JsonUtil.Get<string>(json, "CustomData");
			GrantedIfPlayerHas = JsonUtil.GetList<string>(json, "GrantedIfPlayerHas");
			Consumable = JsonUtil.GetObject<CatalogItemConsumableInfo>(json, "Consumable");
			Container = JsonUtil.GetObject<CatalogItemContainerInfo>(json, "Container");
			Bundle = JsonUtil.GetObject<CatalogItemBundleInfo>(json, "Bundle");
			CanBecomeCharacter = (bool)JsonUtil.Get<bool>(json, "CanBecomeCharacter");
			IsStackable = (bool)JsonUtil.Get<bool>(json, "IsStackable");
		}
	}
	
	
	
	public class CatalogItemBundleInfo : PlayFabModelBase
	{
		
		
		/// <summary>
		/// unique ItemId values for all items which will be added to the player inventory when the bundle is added
		/// </summary>
		
		public List<string> BundledItems { get; set;}
		
		/// <summary>
		/// unique TableId values for all RandomResultTable objects which are part of the bundle (random tables will be resolved and add the relevant items to the player inventory when the bundle is added)
		/// </summary>
		
		public List<string> BundledResultTables { get; set;}
		
		/// <summary>
		/// virtual currency types and balances which will be added to the player inventory when the bundle is added
		/// </summary>
		
		public Dictionary<string,uint> BundledVirtualCurrencies { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("BundledItems", BundledItems);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("BundledResultTables", BundledResultTables);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("BundledVirtualCurrencies", BundledVirtualCurrencies);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			BundledItems = JsonUtil.GetList<string>(json, "BundledItems");
			BundledResultTables = JsonUtil.GetList<string>(json, "BundledResultTables");
			BundledVirtualCurrencies = JsonUtil.GetDictionaryUInt32(json, "BundledVirtualCurrencies");
		}
	}
	
	
	
	public class CatalogItemConsumableInfo : PlayFabModelBase
	{
		
		
		/// <summary>
		/// number of times this object can be used, after which it will be removed from the player inventory
		/// </summary>
		
		public uint? UsageCount { get; set;}
		
		/// <summary>
		/// duration in seconds for how long the item will remain in the player inventory - once elapsed, the item will be removed
		/// </summary>
		
		public uint? UsagePeriod { get; set;}
		
		/// <summary>
		/// all inventory item instances in the player inventory sharing a non-null UsagePeriodGroup have their UsagePeriod values added together, and share the result - when that period has elapsed, all the items in the group will be removed
		/// </summary>
		
		public string UsagePeriodGroup { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("UsageCount", UsageCount);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("UsagePeriod", UsagePeriod);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("UsagePeriodGroup", UsagePeriodGroup);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			UsageCount = (uint?)JsonUtil.Get<double?>(json, "UsageCount");
			UsagePeriod = (uint?)JsonUtil.Get<double?>(json, "UsagePeriod");
			UsagePeriodGroup = (string)JsonUtil.Get<string>(json, "UsagePeriodGroup");
		}
	}
	
	
	
	/// <summary>
	/// Containers are inventory items that can hold other items defined in the catalog, as well as virtual currency, which is added to the player inventory when the container is unlocked, using the UnlockContainerItem API. The items can be anything defined in the catalog, as well as RandomResultTable objects which will be resolved when the container is unlocked. Containers and their keys should be defined as Consumable (having a limited number of uses) in their catalog defintiions, unless the intent is for the player to be able to re-use them infinitely.
	/// </summary>
	public class CatalogItemContainerInfo : PlayFabModelBase
	{
		
		
		/// <summary>
		/// ItemId for the catalog item used to unlock the container, if any (if not specified, a call to UnlockContainerItem will open the container, adding the contents to the player inventory and currency balances)
		/// </summary>
		
		public string KeyItemId { get; set;}
		
		/// <summary>
		/// unique ItemId values for all items which will be added to the player inventory, once the container has been unlocked
		/// </summary>
		
		public List<string> ItemContents { get; set;}
		
		/// <summary>
		/// unique TableId values for all RandomResultTable objects which are part of the container (once unlocked, random tables will be resolved and add the relevant items to the player inventory)
		/// </summary>
		
		public List<string> ResultTableContents { get; set;}
		
		/// <summary>
		/// virtual currency types and balances which will be added to the player inventory when the container is unlocked
		/// </summary>
		
		public Dictionary<string,uint> VirtualCurrencyContents { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("KeyItemId", KeyItemId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemContents", ItemContents);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ResultTableContents", ResultTableContents);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("VirtualCurrencyContents", VirtualCurrencyContents);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			KeyItemId = (string)JsonUtil.Get<string>(json, "KeyItemId");
			ItemContents = JsonUtil.GetList<string>(json, "ItemContents");
			ResultTableContents = JsonUtil.GetList<string>(json, "ResultTableContents");
			VirtualCurrencyContents = JsonUtil.GetDictionaryUInt32(json, "VirtualCurrencyContents");
		}
	}
	
	
	
	public class CharacterLeaderboardEntry : PlayFabModelBase
	{
		
		
		/// <summary>
		/// PlayFab unique identifier of the user for this leaderboard entry.
		/// </summary>
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// PlayFab unique identifier of the character that belongs to the user for this leaderboard entry.
		/// </summary>
		
		public string CharacterId { get; set;}
		
		/// <summary>
		/// Title-specific display name of the character for this leaderboard entry.
		/// </summary>
		
		public string CharacterName { get; set;}
		
		/// <summary>
		/// Title-specific display name of the user for this leaderboard entry.
		/// </summary>
		
		public string DisplayName { get; set;}
		
		/// <summary>
		/// Name of the character class for this entry.
		/// </summary>
		
		public string CharacterType { get; set;}
		
		/// <summary>
		/// Specific value of the user's statistic.
		/// </summary>
		
		public int StatValue { get; set;}
		
		/// <summary>
		/// User's overall position in the leaderboard.
		/// </summary>
		
		public int Position { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterName", CharacterName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("DisplayName", DisplayName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterType", CharacterType);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("StatValue", StatValue);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Position", Position);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			CharacterName = (string)JsonUtil.Get<string>(json, "CharacterName");
			DisplayName = (string)JsonUtil.Get<string>(json, "DisplayName");
			CharacterType = (string)JsonUtil.Get<string>(json, "CharacterType");
			StatValue = (int)JsonUtil.Get<double>(json, "StatValue");
			Position = (int)JsonUtil.Get<double>(json, "Position");
		}
	}
	
	
	
	public class CharacterResult : PlayFabModelBase
	{
		
		
		
		public string CharacterId { get; set;}
		
		
		public string CharacterName { get; set;}
		
		
		public string CharacterType { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterName", CharacterName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterType", CharacterType);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			CharacterName = (string)JsonUtil.Get<string>(json, "CharacterName");
			CharacterType = (string)JsonUtil.Get<string>(json, "CharacterType");
		}
	}
	
	
	
	public class CreateSharedGroupRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the shared group (a random identifier will be assigned, if one is not specified).
		/// </summary>
		
		public string SharedGroupId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("SharedGroupId", SharedGroupId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			SharedGroupId = (string)JsonUtil.Get<string>(json, "SharedGroupId");
		}
	}
	
	
	
	public class CreateSharedGroupResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the shared group.
		/// </summary>
		
		public string SharedGroupId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("SharedGroupId", SharedGroupId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			SharedGroupId = (string)JsonUtil.Get<string>(json, "SharedGroupId");
		}
	}
	
	
	
	public enum Currency
	{
		USD,
		GBP,
		EUR,
		RUB,
		BRL,
		CIS,
		CAD
	}
	
	
	
	public class DeleteCharacterFromUserRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		
		public string CharacterId { get; set;}
		
		/// <summary>
		/// If true, the character's inventory will be transferred up to the owning user; otherwise, this request will purge those items.
		/// </summary>
		
		public bool SaveCharacterInventory { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("SaveCharacterInventory", SaveCharacterInventory);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			SaveCharacterInventory = (bool)JsonUtil.Get<bool>(json, "SaveCharacterInventory");
		}
	}
	
	
	
	public class DeleteCharacterFromUserResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class DeleteSharedGroupRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the shared group.
		/// </summary>
		
		public string SharedGroupId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("SharedGroupId", SharedGroupId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			SharedGroupId = (string)JsonUtil.Get<string>(json, "SharedGroupId");
		}
	}
	
	
	
	public class EmptyResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class GetCatalogItemsRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Which catalog is being requested.
		/// </summary>
		
		public string CatalogVersion { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("CatalogVersion", CatalogVersion);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			CatalogVersion = (string)JsonUtil.Get<string>(json, "CatalogVersion");
		}
	}
	
	
	
	public class GetCatalogItemsResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Array of items which can be purchased.
		/// </summary>
		
		public List<CatalogItem> Catalog { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Catalog", Catalog);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Catalog = JsonUtil.GetObjectList<CatalogItem>(json, "Catalog");
		}
	}
	
	
	
	public class GetCharacterDataRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		
		public string CharacterId { get; set;}
		
		/// <summary>
		/// Specific keys to search for in the custom user data.
		/// </summary>
		
		public List<string> Keys { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Keys", Keys);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			Keys = JsonUtil.GetList<string>(json, "Keys");
		}
	}
	
	
	
	public class GetCharacterDataResult : PlayFabModelBase
	{
		
		
		
		public string CharacterId { get; set;}
		
		/// <summary>
		/// Character specific data for this title.
		/// </summary>
		
		public Dictionary<string,UserDataRecord> Data { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Data", Data);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			Data = JsonUtil.GetObjectDictionary<UserDataRecord>(json, "Data");
		}
	}
	
	
	
	public class GetCharacterInventoryRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		
		public string CharacterId { get; set;}
		
		/// <summary>
		/// Used to limit results to only those from a specific catalog version.
		/// </summary>
		
		public string CatalogVersion { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CatalogVersion", CatalogVersion);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			CatalogVersion = (string)JsonUtil.Get<string>(json, "CatalogVersion");
		}
	}
	
	
	
	public class GetCharacterInventoryResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Array of inventory items belonging to the character.
		/// </summary>
		
		public List<ItemInstance> Inventory { get; set;}
		
		/// <summary>
		/// Array of virtual currency balance(s) belonging to the character.
		/// </summary>
		
		public Dictionary<string,int> VirtualCurrency { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Inventory", Inventory);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("VirtualCurrency", VirtualCurrency);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Inventory = JsonUtil.GetObjectList<ItemInstance>(json, "Inventory");
			VirtualCurrency = JsonUtil.GetDictionaryInt32(json, "VirtualCurrency");
		}
	}
	
	
	
	public class GetCharacterLeaderboardRequest : PlayFabModelBase
	{
		
		
		
		public string CharacterId { get; set;}
		
		/// <summary>
		/// Optional character type on which to filter the leaderboard entries.
		/// </summary>
		
		public string CharacterType { get; set;}
		
		/// <summary>
		/// Unique identifier for the title-specific statistic for the leaderboard.
		/// </summary>
		
		public string StatisticName { get; set;}
		
		/// <summary>
		/// First entry in the leaderboard to be retrieved.
		/// </summary>
		
		public int StartPosition { get; set;}
		
		/// <summary>
		/// Maximum number of entries to retrieve.
		/// </summary>
		
		public int MaxResultsCount { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterType", CharacterType);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("StatisticName", StatisticName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("StartPosition", StartPosition);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("MaxResultsCount", MaxResultsCount);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			CharacterType = (string)JsonUtil.Get<string>(json, "CharacterType");
			StatisticName = (string)JsonUtil.Get<string>(json, "StatisticName");
			StartPosition = (int)JsonUtil.Get<double>(json, "StartPosition");
			MaxResultsCount = (int)JsonUtil.Get<double>(json, "MaxResultsCount");
		}
	}
	
	
	
	public class GetCharacterLeaderboardResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Ordered list of leaderboard entries.
		/// </summary>
		
		public List<CharacterLeaderboardEntry> Leaderboard { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Leaderboard", Leaderboard);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Leaderboard = JsonUtil.GetObjectList<CharacterLeaderboardEntry>(json, "Leaderboard");
		}
	}
	
	
	
	public class GetCharacterStatisticsRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		
		public string CharacterId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
		}
	}
	
	
	
	public class GetCharacterStatisticsResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Character statistics for the requested user.
		/// </summary>
		
		public Dictionary<string,int> CharacterStatistics { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("CharacterStatistics", CharacterStatistics);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			CharacterStatistics = JsonUtil.GetDictionaryInt32(json, "CharacterStatistics");
		}
	}
	
	
	
	public class GetContentDownloadUrlRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Key of the content item to fetch, usually formatted as a path, e.g. images/a.png
		/// </summary>
		
		public string Key { get; set;}
		
		/// <summary>
		/// HTTP method to fetch item - GET or HEAD. Use HEAD when only fetching metadata. Default is GET.
		/// </summary>
		
		public string HttpMethod { get; set;}
		
		/// <summary>
		/// True if download through CDN. CDN provides better download bandwidth and time. However, if you want latest, non-cached version of the content, set this to false. Default is true.
		/// </summary>
		
		public bool? ThruCDN { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Key", Key);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("HttpMethod", HttpMethod);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ThruCDN", ThruCDN);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Key = (string)JsonUtil.Get<string>(json, "Key");
			HttpMethod = (string)JsonUtil.Get<string>(json, "HttpMethod");
			ThruCDN = (bool?)JsonUtil.Get<bool?>(json, "ThruCDN");
		}
	}
	
	
	
	public class GetContentDownloadUrlResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// URL for downloading content via HTTP GET or HEAD method. The URL will expire in 1 hour.
		/// </summary>
		
		public string URL { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("URL", URL);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			URL = (string)JsonUtil.Get<string>(json, "URL");
		}
	}
	
	
	
	public class GetLeaderboardAroundCharacterRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the title-specific statistic for the leaderboard.
		/// </summary>
		
		public string StatisticName { get; set;}
		
		
		public string PlayFabId { get; set;}
		
		
		public string CharacterId { get; set;}
		
		/// <summary>
		/// Optional character type on which to filter the leaderboard entries.
		/// </summary>
		
		public string CharacterType { get; set;}
		
		/// <summary>
		/// Maximum number of entries to retrieve.
		/// </summary>
		
		public int MaxResultsCount { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("StatisticName", StatisticName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterType", CharacterType);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("MaxResultsCount", MaxResultsCount);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			StatisticName = (string)JsonUtil.Get<string>(json, "StatisticName");
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			CharacterType = (string)JsonUtil.Get<string>(json, "CharacterType");
			MaxResultsCount = (int)JsonUtil.Get<double>(json, "MaxResultsCount");
		}
	}
	
	
	
	public class GetLeaderboardAroundCharacterResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Ordered list of leaderboard entries.
		/// </summary>
		
		public List<CharacterLeaderboardEntry> Leaderboard { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Leaderboard", Leaderboard);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Leaderboard = JsonUtil.GetObjectList<CharacterLeaderboardEntry>(json, "Leaderboard");
		}
	}
	
	
	
	public class GetLeaderboardAroundUserRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the title-specific statistic for the leaderboard.
		/// </summary>
		
		public string StatisticName { get; set;}
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Maximum number of entries to retrieve.
		/// </summary>
		
		public int MaxResultsCount { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("StatisticName", StatisticName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("MaxResultsCount", MaxResultsCount);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			StatisticName = (string)JsonUtil.Get<string>(json, "StatisticName");
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			MaxResultsCount = (int)JsonUtil.Get<double>(json, "MaxResultsCount");
		}
	}
	
	
	
	public class GetLeaderboardAroundUserResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Ordered list of leaderboard entries.
		/// </summary>
		
		public List<PlayerLeaderboardEntry> Leaderboard { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Leaderboard", Leaderboard);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Leaderboard = JsonUtil.GetObjectList<PlayerLeaderboardEntry>(json, "Leaderboard");
		}
	}
	
	
	
	public class GetLeaderboardForUsersCharactersRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the title-specific statistic for the leaderboard.
		/// </summary>
		
		public string StatisticName { get; set;}
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Maximum number of entries to retrieve.
		/// </summary>
		
		public int MaxResultsCount { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("StatisticName", StatisticName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("MaxResultsCount", MaxResultsCount);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			StatisticName = (string)JsonUtil.Get<string>(json, "StatisticName");
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			MaxResultsCount = (int)JsonUtil.Get<double>(json, "MaxResultsCount");
		}
	}
	
	
	
	public class GetLeaderboardForUsersCharactersResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Ordered list of leaderboard entries.
		/// </summary>
		
		public List<CharacterLeaderboardEntry> Leaderboard { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Leaderboard", Leaderboard);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Leaderboard = JsonUtil.GetObjectList<CharacterLeaderboardEntry>(json, "Leaderboard");
		}
	}
	
	
	
	public class GetLeaderboardRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the title-specific statistic for the leaderboard.
		/// </summary>
		
		public string StatisticName { get; set;}
		
		/// <summary>
		/// First entry in the leaderboard to be retrieved.
		/// </summary>
		
		public int StartPosition { get; set;}
		
		/// <summary>
		/// Maximum number of entries to retrieve.
		/// </summary>
		
		public int MaxResultsCount { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("StatisticName", StatisticName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("StartPosition", StartPosition);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("MaxResultsCount", MaxResultsCount);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			StatisticName = (string)JsonUtil.Get<string>(json, "StatisticName");
			StartPosition = (int)JsonUtil.Get<double>(json, "StartPosition");
			MaxResultsCount = (int)JsonUtil.Get<double>(json, "MaxResultsCount");
		}
	}
	
	
	
	public class GetLeaderboardResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Ordered list of leaderboard entries.
		/// </summary>
		
		public List<PlayerLeaderboardEntry> Leaderboard { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Leaderboard", Leaderboard);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Leaderboard = JsonUtil.GetObjectList<PlayerLeaderboardEntry>(json, "Leaderboard");
		}
	}
	
	
	
	public class GetPublisherDataRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		///  array of keys to get back data from the Publisher data blob, set by the admin tools
		/// </summary>
		
		public List<string> Keys { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Keys", Keys);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Keys = JsonUtil.GetList<string>(json, "Keys");
		}
	}
	
	
	
	public class GetPublisherDataResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// a dictionary object of key / value pairs
		/// </summary>
		
		public Dictionary<string,string> Data { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Data", Data);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Data = JsonUtil.GetDictionary<string>(json, "Data");
		}
	}
	
	
	
	public class GetSharedGroupDataRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the shared group.
		/// </summary>
		
		public string SharedGroupId { get; set;}
		
		/// <summary>
		/// Specific keys to retrieve from the shared group (if not specified, all keys will be returned, while an empty array indicates that no keys should be returned).
		/// </summary>
		
		public List<string> Keys { get; set;}
		
		/// <summary>
		/// If true, return the list of all members of the shared group.
		/// </summary>
		
		public bool? GetMembers { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("SharedGroupId", SharedGroupId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Keys", Keys);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("GetMembers", GetMembers);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			SharedGroupId = (string)JsonUtil.Get<string>(json, "SharedGroupId");
			Keys = JsonUtil.GetList<string>(json, "Keys");
			GetMembers = (bool?)JsonUtil.Get<bool?>(json, "GetMembers");
		}
	}
	
	
	
	public class GetSharedGroupDataResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Data for the requested keys.
		/// </summary>
		
		public Dictionary<string,SharedGroupDataRecord> Data { get; set;}
		
		/// <summary>
		/// List of PlayFabId identifiers for the members of this group, if requested.
		/// </summary>
		
		public List<string> Members { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Data", Data);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Members", Members);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Data = JsonUtil.GetObjectDictionary<SharedGroupDataRecord>(json, "Data");
			Members = JsonUtil.GetList<string>(json, "Members");
		}
	}
	
	
	
	public class GetTitleDataRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		///  array of keys to get back data from the TitleData data blob, set by the admin tools
		/// </summary>
		
		public List<string> Keys { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Keys", Keys);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Keys = JsonUtil.GetList<string>(json, "Keys");
		}
	}
	
	
	
	public class GetTitleDataResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// a dictionary object of key / value pairs
		/// </summary>
		
		public Dictionary<string,string> Data { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Data", Data);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Data = JsonUtil.GetDictionary<string>(json, "Data");
		}
	}
	
	
	
	public class GetUserAccountInfoRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
		}
	}
	
	
	
	public class GetUserAccountInfoResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Account info for the user whose information was requested.
		/// </summary>
		
		public UserAccountInfo UserInfo { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("UserInfo", UserInfo);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			UserInfo = JsonUtil.GetObject<UserAccountInfo>(json, "UserInfo");
		}
	}
	
	
	
	public class GetUserDataRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Specific keys to search for in the custom user data.
		/// </summary>
		
		public List<string> Keys { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Keys", Keys);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			Keys = JsonUtil.GetList<string>(json, "Keys");
		}
	}
	
	
	
	public class GetUserDataResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// PlayFab unique identifier of the user whose custom data is being returned.
		/// </summary>
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// User specific data for this title.
		/// </summary>
		
		public Dictionary<string,UserDataRecord> Data { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Data", Data);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			Data = JsonUtil.GetObjectDictionary<UserDataRecord>(json, "Data");
		}
	}
	
	
	
	public class GetUserInventoryRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Used to limit results to only those from a specific catalog version.
		/// </summary>
		
		public string CatalogVersion { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CatalogVersion", CatalogVersion);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CatalogVersion = (string)JsonUtil.Get<string>(json, "CatalogVersion");
		}
	}
	
	
	
	public class GetUserInventoryResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Array of inventory items belonging to the user.
		/// </summary>
		
		public List<ItemInstance> Inventory { get; set;}
		
		/// <summary>
		/// Array of virtual currency balance(s) belonging to the user.
		/// </summary>
		
		public Dictionary<string,int> VirtualCurrency { get; set;}
		
		/// <summary>
		/// Array of remaining times and timestamps for virtual currencies.
		/// </summary>
		
		public Dictionary<string,VirtualCurrencyRechargeTime> VirtualCurrencyRechargeTimes { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Inventory", Inventory);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("VirtualCurrency", VirtualCurrency);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("VirtualCurrencyRechargeTimes", VirtualCurrencyRechargeTimes);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Inventory = JsonUtil.GetObjectList<ItemInstance>(json, "Inventory");
			VirtualCurrency = JsonUtil.GetDictionaryInt32(json, "VirtualCurrency");
			VirtualCurrencyRechargeTimes = JsonUtil.GetObjectDictionary<VirtualCurrencyRechargeTime>(json, "VirtualCurrencyRechargeTimes");
		}
	}
	
	
	
	public class GetUserStatisticsRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// User for whom statistics are being requested.
		/// </summary>
		
		public string PlayFabId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
		}
	}
	
	
	
	public class GetUserStatisticsResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// User statistics for the requested user.
		/// </summary>
		
		public Dictionary<string,int> UserStatistics { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("UserStatistics", UserStatistics);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			UserStatistics = JsonUtil.GetDictionaryInt32(json, "UserStatistics");
		}
	}
	
	
	
	public class GrantCharacterToUserRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Non-unique display name of the character being granted.
		/// </summary>
		
		public string CharacterName { get; set;}
		
		/// <summary>
		/// Type of the character being granted; statistics can be sliced based on this value.
		/// </summary>
		
		public string CharacterType { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterName", CharacterName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterType", CharacterType);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterName = (string)JsonUtil.Get<string>(json, "CharacterName");
			CharacterType = (string)JsonUtil.Get<string>(json, "CharacterType");
		}
	}
	
	
	
	public class GrantCharacterToUserResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier tagged to this character.
		/// </summary>
		
		public string CharacterId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
		}
	}
	
	
	
	public class GrantItemsToCharacterRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Catalog version from which items are to be granted.
		/// </summary>
		
		public string CatalogVersion { get; set;}
		
		
		public string CharacterId { get; set;}
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// String detailing any additional information concerning this operation.
		/// </summary>
		
		public string Annotation { get; set;}
		
		/// <summary>
		/// Array of itemIds to grant to the user.
		/// </summary>
		
		public List<string> ItemIds { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("CatalogVersion", CatalogVersion);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Annotation", Annotation);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemIds", ItemIds);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			CatalogVersion = (string)JsonUtil.Get<string>(json, "CatalogVersion");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			Annotation = (string)JsonUtil.Get<string>(json, "Annotation");
			ItemIds = JsonUtil.GetList<string>(json, "ItemIds");
		}
	}
	
	
	
	public class GrantItemsToCharacterResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Array of items granted to users.
		/// </summary>
		
		public List<ItemGrantResult> ItemGrantResults { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("ItemGrantResults", ItemGrantResults);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			ItemGrantResults = JsonUtil.GetObjectList<ItemGrantResult>(json, "ItemGrantResults");
		}
	}
	
	
	
	public class GrantItemsToUserRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Catalog version from which items are to be granted.
		/// </summary>
		
		public string CatalogVersion { get; set;}
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// String detailing any additional information concerning this operation.
		/// </summary>
		
		public string Annotation { get; set;}
		
		/// <summary>
		/// Array of itemIds to grant to the user.
		/// </summary>
		
		public List<string> ItemIds { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("CatalogVersion", CatalogVersion);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Annotation", Annotation);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemIds", ItemIds);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			CatalogVersion = (string)JsonUtil.Get<string>(json, "CatalogVersion");
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			Annotation = (string)JsonUtil.Get<string>(json, "Annotation");
			ItemIds = JsonUtil.GetList<string>(json, "ItemIds");
		}
	}
	
	
	
	public class GrantItemsToUserResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Array of items granted to users.
		/// </summary>
		
		public List<ItemGrantResult> ItemGrantResults { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("ItemGrantResults", ItemGrantResults);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			ItemGrantResults = JsonUtil.GetObjectList<ItemGrantResult>(json, "ItemGrantResults");
		}
	}
	
	
	
	public class GrantItemsToUsersRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Catalog version from which items are to be granted.
		/// </summary>
		
		public string CatalogVersion { get; set;}
		
		/// <summary>
		/// Array of items to grant and the users to whom the items are to be granted.
		/// </summary>
		
		public List<ItemGrant> ItemGrants { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("CatalogVersion", CatalogVersion);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemGrants", ItemGrants);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			CatalogVersion = (string)JsonUtil.Get<string>(json, "CatalogVersion");
			ItemGrants = JsonUtil.GetObjectList<ItemGrant>(json, "ItemGrants");
		}
	}
	
	
	
	public class GrantItemsToUsersResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Array of items granted to users.
		/// </summary>
		
		public List<ItemGrantResult> ItemGrantResults { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("ItemGrantResults", ItemGrantResults);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			ItemGrantResults = JsonUtil.GetObjectList<ItemGrantResult>(json, "ItemGrantResults");
		}
	}
	
	
	
	public class ItemGrant : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Unique identifier of the catalog item to be granted to the user.
		/// </summary>
		
		public string ItemId { get; set;}
		
		/// <summary>
		/// String detailing any additional information concerning this operation.
		/// </summary>
		
		public string Annotation { get; set;}
		
		
		public string CharacterId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemId", ItemId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Annotation", Annotation);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			ItemId = (string)JsonUtil.Get<string>(json, "ItemId");
			Annotation = (string)JsonUtil.Get<string>(json, "Annotation");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
		}
	}
	
	
	
	/// <summary>
	/// Result of granting an item to a user
	/// </summary>
	public class ItemGrantResult : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Unique identifier of the catalog item to be granted to the user.
		/// </summary>
		
		public string ItemId { get; set;}
		
		/// <summary>
		/// Unique instance Id of the granted item.
		/// </summary>
		
		public string ItemInstanceId { get; set;}
		
		/// <summary>
		/// String detailing any additional information concerning this operation.
		/// </summary>
		
		public string Annotation { get; set;}
		
		/// <summary>
		/// Result of this operation.
		/// </summary>
		
		public bool Result { get; set;}
		
		
		public string CharacterId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemId", ItemId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemInstanceId", ItemInstanceId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Annotation", Annotation);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Result", Result);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			ItemId = (string)JsonUtil.Get<string>(json, "ItemId");
			ItemInstanceId = (string)JsonUtil.Get<string>(json, "ItemInstanceId");
			Annotation = (string)JsonUtil.Get<string>(json, "Annotation");
			Result = (bool)JsonUtil.Get<bool>(json, "Result");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
		}
	}
	
	
	
	/// <summary>
	/// A unique instance of an item in a user's inventory
	/// </summary>
	public class ItemInstance : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the inventory item, as defined in the catalog.
		/// </summary>
		
		public string ItemId { get; set;}
		
		/// <summary>
		/// Unique item identifier for this specific instance of the item.
		/// </summary>
		
		public string ItemInstanceId { get; set;}
		
		/// <summary>
		/// Class name for the inventory item, as defined in the catalog.
		/// </summary>
		
		public string ItemClass { get; set;}
		
		/// <summary>
		/// Timestamp for when this instance was purchased.
		/// </summary>
		
		public DateTime? PurchaseDate { get; set;}
		
		/// <summary>
		/// Timestamp for when this instance will expire.
		/// </summary>
		
		public DateTime? Expiration { get; set;}
		
		/// <summary>
		/// Total number of remaining uses, if this is a consumable item.
		/// </summary>
		
		public int? RemainingUses { get; set;}
		
		/// <summary>
		/// Game specific comment associated with this instance when it was added to the user inventory.
		/// </summary>
		
		public string Annotation { get; set;}
		
		/// <summary>
		/// Catalog version for the inventory item, when this instance was created.
		/// </summary>
		
		public string CatalogVersion { get; set;}
		
		/// <summary>
		/// Unique identifier for the parent inventory item, as defined in the catalog, for object which were added from a bundle or container.
		/// </summary>
		
		public string BundleParent { get; set;}
		
		/// <summary>
		/// A set of custom key-value pairs on the inventory item.
		/// </summary>
		
		public Dictionary<string,string> CustomData { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("ItemId", ItemId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemInstanceId", ItemInstanceId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemClass", ItemClass);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("PurchaseDate", PurchaseDate);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Expiration", Expiration);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("RemainingUses", RemainingUses);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Annotation", Annotation);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CatalogVersion", CatalogVersion);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("BundleParent", BundleParent);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CustomData", CustomData);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			ItemId = (string)JsonUtil.Get<string>(json, "ItemId");
			ItemInstanceId = (string)JsonUtil.Get<string>(json, "ItemInstanceId");
			ItemClass = (string)JsonUtil.Get<string>(json, "ItemClass");
			PurchaseDate = (DateTime?)JsonUtil.GetDateTime(json, "PurchaseDate");
			Expiration = (DateTime?)JsonUtil.GetDateTime(json, "Expiration");
			RemainingUses = (int?)JsonUtil.Get<double?>(json, "RemainingUses");
			Annotation = (string)JsonUtil.Get<string>(json, "Annotation");
			CatalogVersion = (string)JsonUtil.Get<string>(json, "CatalogVersion");
			BundleParent = (string)JsonUtil.Get<string>(json, "BundleParent");
			CustomData = JsonUtil.GetDictionary<string>(json, "CustomData");
		}
	}
	
	
	
	public class ListUsersCharactersRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
		}
	}
	
	
	
	public class ListUsersCharactersResult : PlayFabModelBase
	{
		
		
		
		public List<CharacterResult> Characters { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Characters", Characters);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Characters = JsonUtil.GetObjectList<CharacterResult>(json, "Characters");
		}
	}
	
	
	
	public class LogEventRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// PlayFab User Id of the player associated with this event. For non-player associated events, this must be null and EntityId must be set.
		/// </summary>
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// For non player-associated events, a unique ID for the entity associated with this event. For player associated events, this must be null and PlayFabId must be set.
		/// </summary>
		
		public string EntityId { get; set;}
		
		/// <summary>
		/// For non player-associated events, the type of entity associated with this event. For player associated events, this must be null.
		/// </summary>
		
		public string EntityType { get; set;}
		
		/// <summary>
		/// Optional timestamp for this event. If null, the a timestamp is auto-assigned to the event on the server.
		/// </summary>
		
		public DateTime? Timestamp { get; set;}
		
		/// <summary>
		/// A unique event name which will be used as the table name in the Redshift database. The name will be made lower case, and cannot not contain spaces. The use of underscores is recommended, for readability. Events also cannot match reserved terms. The PlayFab reserved terms are 'log_in' and 'purchase', 'create' and 'request', while the Redshift reserved terms can be found here: http://docs.aws.amazon.com/redshift/latest/dg/r_pg_keywords.html.
		/// </summary>
		
		public string EventName { get; set;}
		
		/// <summary>
		/// Contains all the data for this event. Event Values can be strings, booleans or numerics (float, double, integer, long) and must be consistent on a per-event basis (if the Value for Key 'A' in Event 'Foo' is an integer the first time it is sent, it must be an integer in all subsequent 'Foo' events). As with event names, Keys must also not use reserved words (see above). Finally, the size of the Body for an event must be less than 32KB (UTF-8 format).
		/// </summary>
		
		public Dictionary<string,object> Body { get; set;}
		
		/// <summary>
		/// Flag to set event Body as profile details in the Redshift database as well as a standard event.
		/// </summary>
		
		public bool ProfileSetEvent { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("EntityId", EntityId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("EntityType", EntityType);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Timestamp", Timestamp);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("EventName", EventName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Body", Body);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ProfileSetEvent", ProfileSetEvent);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			EntityId = (string)JsonUtil.Get<string>(json, "EntityId");
			EntityType = (string)JsonUtil.Get<string>(json, "EntityType");
			Timestamp = (DateTime?)JsonUtil.GetDateTime(json, "Timestamp");
			EventName = (string)JsonUtil.Get<string>(json, "EventName");
			Body = JsonUtil.GetDictionary<object>(json, "Body");
			ProfileSetEvent = (bool)JsonUtil.Get<bool>(json, "ProfileSetEvent");
		}
	}
	
	
	
	public class LogEventResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class ModifyCharacterVirtualCurrencyResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Name of the virtual currency which was modified.
		/// </summary>
		
		public string VirtualCurrency { get; set;}
		
		/// <summary>
		/// Balance of the virtual currency after modification.
		/// </summary>
		
		public int Balance { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("VirtualCurrency", VirtualCurrency);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Balance", Balance);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			VirtualCurrency = (string)JsonUtil.Get<string>(json, "VirtualCurrency");
			Balance = (int)JsonUtil.Get<double>(json, "Balance");
		}
	}
	
	
	
	public class ModifyItemUsesRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// PlayFab unique identifier of the user whose item is being modified.
		/// </summary>
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Unique instance identifier of the item to be modified.
		/// </summary>
		
		public string ItemInstanceId { get; set;}
		
		/// <summary>
		/// Number of uses to add to the item. Can be negative to remove uses.
		/// </summary>
		
		public int UsesToAdd { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemInstanceId", ItemInstanceId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("UsesToAdd", UsesToAdd);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			ItemInstanceId = (string)JsonUtil.Get<string>(json, "ItemInstanceId");
			UsesToAdd = (int)JsonUtil.Get<double>(json, "UsesToAdd");
		}
	}
	
	
	
	public class ModifyItemUsesResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique instance identifier of the item with uses consumed.
		/// </summary>
		
		public string ItemInstanceId { get; set;}
		
		/// <summary>
		/// Number of uses remaining on the item.
		/// </summary>
		
		public int RemainingUses { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("ItemInstanceId", ItemInstanceId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("RemainingUses", RemainingUses);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			ItemInstanceId = (string)JsonUtil.Get<string>(json, "ItemInstanceId");
			RemainingUses = (int)JsonUtil.Get<double>(json, "RemainingUses");
		}
	}
	
	
	
	public class ModifyUserVirtualCurrencyResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// User currency was subtracted from.
		/// </summary>
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Name of the virtual currency which was modified.
		/// </summary>
		
		public string VirtualCurrency { get; set;}
		
		/// <summary>
		/// Amount added or subtracted from the user's virtual currency.
		/// </summary>
		
		public int BalanceChange { get; set;}
		
		/// <summary>
		/// Balance of the virtual currency after modification.
		/// </summary>
		
		public int Balance { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("VirtualCurrency", VirtualCurrency);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("BalanceChange", BalanceChange);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Balance", Balance);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			VirtualCurrency = (string)JsonUtil.Get<string>(json, "VirtualCurrency");
			BalanceChange = (int)JsonUtil.Get<double>(json, "BalanceChange");
			Balance = (int)JsonUtil.Get<double>(json, "Balance");
		}
	}
	
	
	
	public class MoveItemToCharacterFromCharacterRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Unique identifier of the character that currently has the item.
		/// </summary>
		
		public string GivingCharacterId { get; set;}
		
		/// <summary>
		/// Unique identifier of the character that will be receiving the item.
		/// </summary>
		
		public string ReceivingCharacterId { get; set;}
		
		
		public string ItemInstanceId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("GivingCharacterId", GivingCharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ReceivingCharacterId", ReceivingCharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemInstanceId", ItemInstanceId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			GivingCharacterId = (string)JsonUtil.Get<string>(json, "GivingCharacterId");
			ReceivingCharacterId = (string)JsonUtil.Get<string>(json, "ReceivingCharacterId");
			ItemInstanceId = (string)JsonUtil.Get<string>(json, "ItemInstanceId");
		}
	}
	
	
	
	public class MoveItemToCharacterFromCharacterResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class MoveItemToCharacterFromUserRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		
		public string CharacterId { get; set;}
		
		
		public string ItemInstanceId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemInstanceId", ItemInstanceId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			ItemInstanceId = (string)JsonUtil.Get<string>(json, "ItemInstanceId");
		}
	}
	
	
	
	public class MoveItemToCharacterFromUserResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class MoveItemToUserFromCharacterRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		
		public string CharacterId { get; set;}
		
		
		public string ItemInstanceId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemInstanceId", ItemInstanceId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			ItemInstanceId = (string)JsonUtil.Get<string>(json, "ItemInstanceId");
		}
	}
	
	
	
	public class MoveItemToUserFromCharacterResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class NotifyMatchmakerPlayerLeftRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier of the Game Instance the user is leaving.
		/// </summary>
		
		public string LobbyId { get; set;}
		
		
		public string PlayFabId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("LobbyId", LobbyId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			LobbyId = (string)JsonUtil.Get<string>(json, "LobbyId");
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
		}
	}
	
	
	
	public class NotifyMatchmakerPlayerLeftResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// State of user leaving the Game Server Instance.
		/// </summary>
		
		public PlayerConnectionState? PlayerState { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayerState", PlayerState);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayerState = (PlayerConnectionState?)JsonUtil.GetEnum<PlayerConnectionState>(json, "PlayerState");
		}
	}
	
	
	
	public enum PlayerConnectionState
	{
		Unassigned,
		Connecting,
		Participating,
		Participated,
		Reconnecting
	}
	
	
	
	public class PlayerLeaderboardEntry : PlayFabModelBase
	{
		
		
		/// <summary>
		/// PlayFab unique identifier of the user for this leaderboard entry.
		/// </summary>
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Title-specific display name of the user for this leaderboard entry.
		/// </summary>
		
		public string DisplayName { get; set;}
		
		/// <summary>
		/// Specific value of the user's statistic.
		/// </summary>
		
		public int StatValue { get; set;}
		
		/// <summary>
		/// User's overall position in the leaderboard.
		/// </summary>
		
		public int Position { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("DisplayName", DisplayName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("StatValue", StatValue);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Position", Position);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			DisplayName = (string)JsonUtil.Get<string>(json, "DisplayName");
			StatValue = (int)JsonUtil.Get<double>(json, "StatValue");
			Position = (int)JsonUtil.Get<double>(json, "Position");
		}
	}
	
	
	
	public class RedeemMatchmakerTicketRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Server authorization ticket passed back from a call to Matchmake or StartGame.
		/// </summary>
		
		public string Ticket { get; set;}
		
		/// <summary>
		/// Unique identifier of the Game Server Instance that is asking for validation of the authorization ticket.
		/// </summary>
		
		public string LobbyId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Ticket", Ticket);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("LobbyId", LobbyId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Ticket = (string)JsonUtil.Get<string>(json, "Ticket");
			LobbyId = (string)JsonUtil.Get<string>(json, "LobbyId");
		}
	}
	
	
	
	public class RedeemMatchmakerTicketResult : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Boolean indicating whether the ticket was validated by the PlayFab service.
		/// </summary>
		
		public bool TicketIsValid { get; set;}
		
		/// <summary>
		/// Error value if the ticket was not validated.
		/// </summary>
		
		public string Error { get; set;}
		
		/// <summary>
		/// User account information for the user validated.
		/// </summary>
		
		public UserAccountInfo UserInfo { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("TicketIsValid", TicketIsValid);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Error", Error);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("UserInfo", UserInfo);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			TicketIsValid = (bool)JsonUtil.Get<bool>(json, "TicketIsValid");
			Error = (string)JsonUtil.Get<string>(json, "Error");
			UserInfo = JsonUtil.GetObject<UserAccountInfo>(json, "UserInfo");
		}
	}
	
	
	
	public class RemoveSharedGroupMembersRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the shared group.
		/// </summary>
		
		public string SharedGroupId { get; set;}
		
		
		public List<string> PlayFabIds { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("SharedGroupId", SharedGroupId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("PlayFabIds", PlayFabIds);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			SharedGroupId = (string)JsonUtil.Get<string>(json, "SharedGroupId");
			PlayFabIds = JsonUtil.GetList<string>(json, "PlayFabIds");
		}
	}
	
	
	
	public class RemoveSharedGroupMembersResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class ReportPlayerServerRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// PlayFabId of the reporting player.
		/// </summary>
		
		public string ReporterId { get; set;}
		
		/// <summary>
		/// PlayFabId of the reported player.
		/// </summary>
		
		public string ReporteeId { get; set;}
		
		/// <summary>
		/// Title player was reported in, optional if report not for specific title.
		/// </summary>
		
		public string TitleId { get; set;}
		
		/// <summary>
		/// Optional additional comment by reporting player.
		/// </summary>
		
		public string Comment { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("ReporterId", ReporterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ReporteeId", ReporteeId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("TitleId", TitleId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Comment", Comment);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			ReporterId = (string)JsonUtil.Get<string>(json, "ReporterId");
			ReporteeId = (string)JsonUtil.Get<string>(json, "ReporteeId");
			TitleId = (string)JsonUtil.Get<string>(json, "TitleId");
			Comment = (string)JsonUtil.Get<string>(json, "Comment");
		}
	}
	
	
	
	public class ReportPlayerServerResult : PlayFabModelBase
	{
		
		
		
		public bool Updated { get; set;}
		
		
		public int SubmissionsRemaining { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Updated", Updated);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("SubmissionsRemaining", SubmissionsRemaining);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Updated = (bool)JsonUtil.Get<bool>(json, "Updated");
			SubmissionsRemaining = (int)JsonUtil.Get<double>(json, "SubmissionsRemaining");
		}
	}
	
	
	
	public class SendPushNotificationRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// PlayFabId of the recipient of the push notification.
		/// </summary>
		
		public string Recipient { get; set;}
		
		/// <summary>
		/// Text of message to send.
		/// </summary>
		
		public string Message { get; set;}
		
		/// <summary>
		/// Subject of message to send (may not be displayed in all platforms.
		/// </summary>
		
		public string Subject { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Recipient", Recipient);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Message", Message);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Subject", Subject);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Recipient = (string)JsonUtil.Get<string>(json, "Recipient");
			Message = (string)JsonUtil.Get<string>(json, "Message");
			Subject = (string)JsonUtil.Get<string>(json, "Subject");
		}
	}
	
	
	
	public class SendPushNotificationResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class SetPublisherDataRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// key we want to set a value on (note, this is additive - will only replace an existing key's value if they are the same name.) Keys are trimmed of whitespace. Keys may not begin with the '!' character.
		/// </summary>
		
		public string Key { get; set;}
		
		/// <summary>
		/// new value to set. Set to null to remove a value
		/// </summary>
		
		public string Value { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Key", Key);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Value", Value);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Key = (string)JsonUtil.Get<string>(json, "Key");
			Value = (string)JsonUtil.Get<string>(json, "Value");
		}
	}
	
	
	
	public class SetPublisherDataResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class SetTitleDataRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// key we want to set a value on (note, this is additive - will only replace an existing key's value if they are the same name.) Keys are trimmed of whitespace. Keys may not begin with the '!' character.
		/// </summary>
		
		public string Key { get; set;}
		
		/// <summary>
		/// new value to set. Set to null to remove a value
		/// </summary>
		
		public string Value { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Key", Key);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Value", Value);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Key = (string)JsonUtil.Get<string>(json, "Key");
			Value = (string)JsonUtil.Get<string>(json, "Value");
		}
	}
	
	
	
	public class SetTitleDataResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class SharedGroupDataRecord : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Data stored for the specified group data key.
		/// </summary>
		
		public string Value { get; set;}
		
		/// <summary>
		/// PlayFabId of the user to last update this value.
		/// </summary>
		
		public string LastUpdatedBy { get; set;}
		
		/// <summary>
		/// Timestamp for when this data was last updated.
		/// </summary>
		
		public DateTime LastUpdated { get; set;}
		
		/// <summary>
		/// Indicates whether this data can be read by all users (public) or only members of the group (private).
		/// </summary>
		
		public UserDataPermission? Permission { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Value", Value);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("LastUpdatedBy", LastUpdatedBy);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("LastUpdated", LastUpdated);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Permission", Permission);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Value = (string)JsonUtil.Get<string>(json, "Value");
			LastUpdatedBy = (string)JsonUtil.Get<string>(json, "LastUpdatedBy");
			LastUpdated = (DateTime)JsonUtil.GetDateTime(json, "LastUpdated");
			Permission = (UserDataPermission?)JsonUtil.GetEnum<UserDataPermission>(json, "Permission");
		}
	}
	
	
	
	public class SubtractCharacterVirtualCurrencyRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		
		public string CharacterId { get; set;}
		
		/// <summary>
		/// Name of the virtual currency which is to be decremented.
		/// </summary>
		
		public string VirtualCurrency { get; set;}
		
		/// <summary>
		/// Amount to be subtracted from the user balance of the specified virtual currency.
		/// </summary>
		
		public int Amount { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("VirtualCurrency", VirtualCurrency);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Amount", Amount);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			VirtualCurrency = (string)JsonUtil.Get<string>(json, "VirtualCurrency");
			Amount = (int)JsonUtil.Get<double>(json, "Amount");
		}
	}
	
	
	
	public class SubtractUserVirtualCurrencyRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// PlayFab unique identifier of the user whose virtual currency balance is to be decreased.
		/// </summary>
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Name of the virtual currency which is to be decremented.
		/// </summary>
		
		public string VirtualCurrency { get; set;}
		
		/// <summary>
		/// Amount to be subtracted from the user balance of the specified virtual currency.
		/// </summary>
		
		public int Amount { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("VirtualCurrency", VirtualCurrency);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Amount", Amount);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			VirtualCurrency = (string)JsonUtil.Get<string>(json, "VirtualCurrency");
			Amount = (int)JsonUtil.Get<double>(json, "Amount");
		}
	}
	
	
	
	public enum TitleActivationStatus
	{
		None,
		ActivatedTitleKey,
		PendingSteam,
		ActivatedSteam,
		RevokedSteam
	}
	
	
	
	public class UpdateCharacterDataRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		
		public string CharacterId { get; set;}
		
		/// <summary>
		/// Data to be written to the user's character's custom data. Note that keys are trimmed of whitespace, are limited to 1024 characters, and may not begin with a '!' character.
		/// </summary>
		
		public Dictionary<string,string> Data { get; set;}
		
		/// <summary>
		/// Permission to be applied to all user data keys written in this request. Defaults to "private" if not set.
		/// </summary>
		
		public UserDataPermission? Permission { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Data", Data);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Permission", Permission);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			Data = JsonUtil.GetDictionary<string>(json, "Data");
			Permission = (UserDataPermission?)JsonUtil.GetEnum<UserDataPermission>(json, "Permission");
		}
	}
	
	
	
	public class UpdateCharacterDataResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class UpdateCharacterStatisticsRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		
		public string CharacterId { get; set;}
		
		/// <summary>
		/// Statistics to be updated with the provided values.
		/// </summary>
		
		public Dictionary<string,int> CharacterStatistics { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("CharacterStatistics", CharacterStatistics);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			CharacterStatistics = JsonUtil.GetDictionaryInt32(json, "CharacterStatistics");
		}
	}
	
	
	
	public class UpdateCharacterStatisticsResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class UpdateSharedGroupDataRequest : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Unique identifier for the shared group.
		/// </summary>
		
		public string SharedGroupId { get; set;}
		
		/// <summary>
		/// Key value pairs to be stored in the shared group - note that keys will be trimmed of whitespace, must not begin with a '!' character, and that null values will result in the removal of the key from the data set.
		/// </summary>
		
		public Dictionary<string,string> Data { get; set;}
		
		/// <summary>
		/// Permission to be applied to all user data keys in this request.
		/// </summary>
		
		public UserDataPermission? Permission { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("SharedGroupId", SharedGroupId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Data", Data);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Permission", Permission);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			SharedGroupId = (string)JsonUtil.Get<string>(json, "SharedGroupId");
			Data = JsonUtil.GetDictionary<string>(json, "Data");
			Permission = (UserDataPermission?)JsonUtil.GetEnum<UserDataPermission>(json, "Permission");
		}
	}
	
	
	
	public class UpdateSharedGroupDataResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class UpdateUserDataRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Data to be written to the user's custom data. Note that keys are trimmed of whitespace, are limited to 1024 characters, and may not begin with a '!' character.
		/// </summary>
		
		public Dictionary<string,string> Data { get; set;}
		
		/// <summary>
		/// Permission to be applied to all user data keys written in this request. Defaults to "private" if not set.
		/// </summary>
		
		public UserDataPermission? Permission { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Data", Data);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Permission", Permission);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			Data = JsonUtil.GetDictionary<string>(json, "Data");
			Permission = (UserDataPermission?)JsonUtil.GetEnum<UserDataPermission>(json, "Permission");
		}
	}
	
	
	
	public class UpdateUserDataResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class UpdateUserInternalDataRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Data to be written to the user's custom data.
		/// </summary>
		
		public Dictionary<string,string> Data { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Data", Data);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			Data = JsonUtil.GetDictionary<string>(json, "Data");
		}
	}
	
	
	
	public class UpdateUserInventoryItemDataRequest : PlayFabModelBase
	{
		
		
		
		public string CharacterId { get; set;}
		
		
		public string PlayFabId { get; set;}
		
		
		public string ItemInstanceId { get; set;}
		
		/// <summary>
		/// Data to be written to the item's custom data. Note that keys are trimmed of whitespace.
		/// </summary>
		
		public Dictionary<string,string> Data { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("CharacterId", CharacterId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("ItemInstanceId", ItemInstanceId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Data", Data);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			CharacterId = (string)JsonUtil.Get<string>(json, "CharacterId");
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			ItemInstanceId = (string)JsonUtil.Get<string>(json, "ItemInstanceId");
			Data = JsonUtil.GetDictionary<string>(json, "Data");
		}
	}
	
	
	
	public class UpdateUserInventoryItemDataResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class UpdateUserStatisticsRequest : PlayFabModelBase
	{
		
		
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// Statistics to be updated with the provided values.
		/// </summary>
		
		public Dictionary<string,int> UserStatistics { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("UserStatistics", UserStatistics);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			UserStatistics = JsonUtil.GetDictionaryInt32(json, "UserStatistics");
		}
	}
	
	
	
	public class UpdateUserStatisticsResult : PlayFabModelBase
	{
		
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
		}
	}
	
	
	
	public class UserAccountInfo : PlayFabModelBase
	{
		
		
		/// <summary>
		/// unique identifier for the user account
		/// </summary>
		
		public string PlayFabId { get; set;}
		
		/// <summary>
		/// timestamp indicating when the user account was created
		/// </summary>
		
		public DateTime Created { get; set;}
		
		/// <summary>
		/// user account name in the PlayFab service
		/// </summary>
		
		public string Username { get; set;}
		
		/// <summary>
		/// title-specific information for the user account
		/// </summary>
		
		public UserTitleInfo TitleInfo { get; set;}
		
		/// <summary>
		/// personal information for the user which is considered more sensitive
		/// </summary>
		
		public UserPrivateAccountInfo PrivateInfo { get; set;}
		
		/// <summary>
		/// user Facebook information, if a Facebook account has been linked
		/// </summary>
		
		public UserFacebookInfo FacebookInfo { get; set;}
		
		/// <summary>
		/// user Steam information, if a Steam account has been linked
		/// </summary>
		
		public UserSteamInfo SteamInfo { get; set;}
		
		/// <summary>
		/// user Gamecenter information, if a Gamecenter account has been linked
		/// </summary>
		
		public UserGameCenterInfo GameCenterInfo { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("PlayFabId", PlayFabId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Created", Created);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Username", Username);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("TitleInfo", TitleInfo);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("PrivateInfo", PrivateInfo);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("FacebookInfo", FacebookInfo);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("SteamInfo", SteamInfo);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("GameCenterInfo", GameCenterInfo);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			PlayFabId = (string)JsonUtil.Get<string>(json, "PlayFabId");
			Created = (DateTime)JsonUtil.GetDateTime(json, "Created");
			Username = (string)JsonUtil.Get<string>(json, "Username");
			TitleInfo = JsonUtil.GetObject<UserTitleInfo>(json, "TitleInfo");
			PrivateInfo = JsonUtil.GetObject<UserPrivateAccountInfo>(json, "PrivateInfo");
			FacebookInfo = JsonUtil.GetObject<UserFacebookInfo>(json, "FacebookInfo");
			SteamInfo = JsonUtil.GetObject<UserSteamInfo>(json, "SteamInfo");
			GameCenterInfo = JsonUtil.GetObject<UserGameCenterInfo>(json, "GameCenterInfo");
		}
	}
	
	
	
	public enum UserDataPermission
	{
		Private,
		Public
	}
	
	
	
	public class UserDataRecord : PlayFabModelBase
	{
		
		
		/// <summary>
		/// User-supplied data for this user data key.
		/// </summary>
		
		public string Value { get; set;}
		
		/// <summary>
		/// Timestamp indicating when this data was last updated.
		/// </summary>
		
		public DateTime LastUpdated { get; set;}
		
		/// <summary>
		/// Permissions on this data key.
		/// </summary>
		
		public UserDataPermission? Permission { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Value", Value);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("LastUpdated", LastUpdated);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Permission", Permission);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Value = (string)JsonUtil.Get<string>(json, "Value");
			LastUpdated = (DateTime)JsonUtil.GetDateTime(json, "LastUpdated");
			Permission = (UserDataPermission?)JsonUtil.GetEnum<UserDataPermission>(json, "Permission");
		}
	}
	
	
	
	public class UserFacebookInfo : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Facebook identifier
		/// </summary>
		
		public string FacebookId { get; set;}
		
		/// <summary>
		/// Facebook full name
		/// </summary>
		
		public string FullName { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("FacebookId", FacebookId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("FullName", FullName);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			FacebookId = (string)JsonUtil.Get<string>(json, "FacebookId");
			FullName = (string)JsonUtil.Get<string>(json, "FullName");
		}
	}
	
	
	
	public class UserGameCenterInfo : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Gamecenter identifier
		/// </summary>
		
		public string GameCenterId { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("GameCenterId", GameCenterId);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			GameCenterId = (string)JsonUtil.Get<string>(json, "GameCenterId");
		}
	}
	
	
	
	public enum UserOrigination
	{
		Organic,
		Steam,
		Google,
		Amazon,
		Facebook,
		Kongregate,
		GamersFirst,
		Unknown,
		IOS,
		LoadTest,
		Android,
		PSN,
		GameCenter
	}
	
	
	
	public class UserPrivateAccountInfo : PlayFabModelBase
	{
		
		
		/// <summary>
		/// user email address
		/// </summary>
		
		public string Email { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("Email", Email);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			Email = (string)JsonUtil.Get<string>(json, "Email");
		}
	}
	
	
	
	public class UserSteamInfo : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Steam identifier
		/// </summary>
		
		public string SteamId { get; set;}
		
		/// <summary>
		/// the country in which the player resides, from Steam data
		/// </summary>
		
		public string SteamCountry { get; set;}
		
		/// <summary>
		/// currency type set in the user Steam account
		/// </summary>
		
		public Currency? SteamCurrency { get; set;}
		
		/// <summary>
		/// what stage of game ownership the user is listed as being in, from Steam
		/// </summary>
		
		public TitleActivationStatus? SteamActivationStatus { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("SteamId", SteamId);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("SteamCountry", SteamCountry);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("SteamCurrency", SteamCurrency);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("SteamActivationStatus", SteamActivationStatus);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			SteamId = (string)JsonUtil.Get<string>(json, "SteamId");
			SteamCountry = (string)JsonUtil.Get<string>(json, "SteamCountry");
			SteamCurrency = (Currency?)JsonUtil.GetEnum<Currency>(json, "SteamCurrency");
			SteamActivationStatus = (TitleActivationStatus?)JsonUtil.GetEnum<TitleActivationStatus>(json, "SteamActivationStatus");
		}
	}
	
	
	
	public class UserTitleInfo : PlayFabModelBase
	{
		
		
		/// <summary>
		/// name of the user, as it is displayed in-game
		/// </summary>
		
		public string DisplayName { get; set;}
		
		/// <summary>
		/// source by which the user first joined the game, if known
		/// </summary>
		
		public UserOrigination? Origination { get; set;}
		
		/// <summary>
		/// timestamp indicating when the user was first associated with this game (this can differ significantly from when the user first registered with PlayFab)
		/// </summary>
		
		public DateTime Created { get; set;}
		
		/// <summary>
		/// timestamp for the last user login for this title
		/// </summary>
		
		public DateTime? LastLogin { get; set;}
		
		/// <summary>
		/// timestamp indicating when the user first signed into this game (this can differ from the Created timestamp, as other events, such as issuing a beta key to the user, can associate the title to the user)
		/// </summary>
		
		public DateTime? FirstLogin { get; set;}
		
		/// <summary>
		/// boolean indicating whether or not the user is currently banned for a title
		/// </summary>
		
		public bool? isBanned { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("DisplayName", DisplayName);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Origination", Origination);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("Created", Created);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("LastLogin", LastLogin);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("FirstLogin", FirstLogin);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("isBanned", isBanned);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			DisplayName = (string)JsonUtil.Get<string>(json, "DisplayName");
			Origination = (UserOrigination?)JsonUtil.GetEnum<UserOrigination>(json, "Origination");
			Created = (DateTime)JsonUtil.GetDateTime(json, "Created");
			LastLogin = (DateTime?)JsonUtil.GetDateTime(json, "LastLogin");
			FirstLogin = (DateTime?)JsonUtil.GetDateTime(json, "FirstLogin");
			isBanned = (bool?)JsonUtil.Get<bool?>(json, "isBanned");
		}
	}
	
	
	
	public class VirtualCurrencyRechargeTime : PlayFabModelBase
	{
		
		
		/// <summary>
		/// Time remaining (in seconds) before the next recharge increment of the virtual currency.
		/// </summary>
		
		public int SecondsToRecharge { get; set;}
		
		/// <summary>
		/// Server timestamp in UTC indicating the next time the virtual currency will be incremented.
		/// </summary>
		
		public DateTime RechargeTime { get; set;}
		
		/// <summary>
		/// Maximum value to which the regenerating currency will automatically increment. Note that it can exceed this value through use of the AddUserVirtualCurrency API call. However, it will not regenerate automatically until it has fallen below this value.
		/// </summary>
		
		public int RechargeMax { get; set;}
		
		public override void WriteJson(JsonWriter writer)
		{
			writer.Writer.Write(JsonReader.OperatorObjectStart);
			
			writer.WriteObjectProperty("SecondsToRecharge", SecondsToRecharge);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("RechargeTime", RechargeTime);
			
			writer.Writer.Write(JsonReader.OperatorValueDelim);
			
			writer.WriteObjectProperty("RechargeMax", RechargeMax);
			
			writer.Writer.Write(JsonReader.OperatorObjectEnd);
		}
		
		public override void Deserialize (Dictionary<string,object> json)
		{
			
			SecondsToRecharge = (int)JsonUtil.Get<double>(json, "SecondsToRecharge");
			RechargeTime = (DateTime)JsonUtil.GetDateTime(json, "RechargeTime");
			RechargeMax = (int)JsonUtil.Get<double>(json, "RechargeMax");
		}
	}
	
}
