handlers.RoomCreated = function(args)
{
	var stats = server.GetUserStatistics({"PlayFabId" : currentPlayerId});
	var redTeamJoinedCount = parseInt(stats.UserStatistics.RedTeamJoinedCount !== undefined ? stats.UserStatistics.RedTeamJoinedCount : 0);
	var bluTeamJoinedCount = parseInt(stats.UserStatistics.BluTeamJoinedCount !== undefined ? stats.UserStatistics.BluTeamJoinedCount : 0);

	// pick a team
	var team = (Math.random() * (100 - 1) + 1) > 50 ? "Red" : "Blu";

	// write player stats to increment event counters
	if(team === "Red")
	{
		server.UpdateUserStatistics({"PlayFabId" : currentPlayerId, "UserStatistics" : { "RedTeamJoinedCount" : redTeamJoinedCount + 1 } });	
	}
	if(team === "Blu")
	{
		server.UpdateUserStatistics({"PlayFabId" : currentPlayerId, "UserStatistics" : { "BluTeamJoinedCount" : bluTeamJoinedCount + 1 } });	
	}
}

handlers.RoomClosed = function(args)
{
	var stats = server.GetUserStatistics({"PlayFabId" : currentPlayerId});
	var roomClosedEventRaised = parseInt(stats.UserStatistics.roomClosedEventRaised !== undefined ? stats.UserStatistics.roomClosedEventRaised : 0);

	// write player stats to increment event counters
	server.UpdateUserStatistics({"PlayFabId" : currentPlayerId, "UserStatistics" : {
		"roomClosedEventRaised" : roomClosedEventRaised + 1,
		} 
	});	
}

handlers.RoomEventRaised = function(args)
{
    var stats = server.GetUserStatistics({"PlayFabId" : currentPlayerId});
    if(typeof args.Data.eventType !== undefined)
    {
    	if(args.Data.eventType === "exp")
    	{
			// write player stats to increment event counters
			var experiencePoints = parseInt(stats.UserStatistics.ExperiencePoints !== undefined ? stats.UserStatistics.ExperiencePoints : 0);

			var exp = parseInt(args.Data.amt !== undefined ? args.Data.amt : 0);

			// write player stats to increment event counters
			server.UpdateUserStatistics({"PlayFabId" : currentPlayerId, "UserStatistics" : {
				"ExperiencePoints" : experiencePoints + exp,
				} 
			});	
    	}
		else if(args.Data.eventType === "mvp")
		{
			var mVPsWonCount = parseInt(stats.UserStatistics.MVPsWonCount !== undefined ? stats.UserStatistics.MVPsWonCount : 0);

			// write player stats to increment event counters
			server.UpdateUserStatistics({"PlayFabId" : currentPlayerId, "UserStatistics" : {
				"MVPsWonCount" : mVPsWonCount + 1,
				} 
			});	
		}
    }
}

handlers.RoomPropertyUpdated = function(args)
{
	var stats = server.GetUserStatistics({"PlayFabId" : currentPlayerId});
	var roomPropertyUpdatedEventRaised = parseInt(stats.UserStatistics.roomPropertyUpdatedEventRaised !== undefined ? stats.UserStatistics.roomPropertyUpdatedEventRaised : 0);

	// write player stats to increment event counters
	server.UpdateUserStatistics({"PlayFabId" : currentPlayerId, "UserStatistics" : {
		"roomPropertyUpdatedEventRaised" : roomPropertyUpdatedEventRaised + 1,
		} 
	});	
}
