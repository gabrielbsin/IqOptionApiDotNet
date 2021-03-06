﻿﻿using System;
using System.Threading.Tasks;
using IqOptionApiDotNet.Http;
using IqOptionApiDotNet.Models;
using IqOptionApiDotNet.Models.BinaryOptions;
using IqOptionApiDotNet.Models.DigitalOptions;
using IqOptionApiDotNet.Ws;

namespace IqOptionApiDotNet
{
    public interface IIqOptionApiDotNetClient : IDisposable
    {
        IqOptionWebSocketClient WsClient { get; }
        IqOptionHttpClient HttpClient { get; }
        IObservable<Profile> ProfileObservable { get; }
        Profile Profile { get; }
        bool IsConnected { get; }
        IObservable<bool> ConnectedObservable { get; }

        Task<bool> ConnectAsync();
        Task<Profile> GetProfileAsync(string requestId);
		
		Task<UserProfileClientResult> GetUserProfileClientAsync(string requestId, long userId);
        Task<LeaderBoardDealsClientResult> RequestLeaderboardDealsClientAsync(string requestId, long countryId, long userCountryId, long fromPosition, long toPosition, long nearTradersCountryCount, long nearTradersCount, long topCountryCount, long topCount, long topType);
        Task<LeaderBoardUserinfoDealsClientMessageResult> RequestLeaderboardUserinfoDealsClientAsync(string requestId, long[] countryId, int userId);
        Task<UsersAvailabilityResult> GetUsersAvailabilityAsync(string requestId, long[] userId);
		Task<FinancialInformationResult> GetFinancialInformationAsync(string requestId, ActivePair pair);
        Task<bool> ChangeBalanceAsync(string requestId, long balanceId);
        Task<BinaryOptionsResult> BuyAsync(string requestId, ActivePair pair, int size, OrderDirection direction, DateTimeOffset expiration);
        Task<CandleCollections> GetCandlesAsync(string requestId, ActivePair pair, TimeFrame tf, int count, DateTimeOffset to);
        Task<IObservable<CurrentCandle>> SubscribeRealtimeQuoteAsync(string requestId, ActivePair pair, TimeFrame tf);

        Task UnSubscribeRealtimeData(string requestId, ActivePair pair, TimeFrame tf);

        #region Subscribe

        /// <summary>
        /// Subscribe traders mood changed
        /// </summary>
        /// <param name="requestId">Request identifier<example>5f2c370145a047c7b87f2680556b3b93</example></param>
        /// <param name="instrumentId">The Instrument identifier <example>doEURUSD201907191250PT5MPSPT</example></param>
        /// <param name="pair">The Active pair, make sure your place with correct active.</param>
        void SubscribeTradersMoodChanged(string requestId, InstrumentType instrumentType, ActivePair active);

        /// <summary>
        /// Unsubscribe traders mood changed
        /// </summary>
        /// <param name="requestId">Request identifier<example>5f2c370145a047c7b87f2680556b3b93</example></param>
        /// <param name="instrumentId">The Instrument identifier <example>doEURUSD201907191250PT5MPSPT</example></param>
        /// <param name="pair">The Active pair, make sure your place with correct active.</param>
        void UnSubscribeTradersMoodChanged(string requestId, InstrumentType instrumentType, ActivePair active);

        /// <summary>
        /// Subscribe live deal
        /// </summary>
        /// <param name="requestId">Request identifier<example>5f2c370145a047c7b87f2680556b3b93</example></param>
        void SubscribeLiveDeal(string requestId, string message, ActivePair pair, DigitalOptionsExpiryType duration);

        /// <summary>
        /// Unsubscribe live deal
        /// </summary>
        /// <param name="requestId">Request identifier<example>5f2c370145a047c7b87f2680556b3b93</example></param>
        void UnSubscribeLiveDeal(string requestId, string message, ActivePair pair, DigitalOptionsExpiryType duration);


        #endregion

        #region PlacePositionCommands

        /// <summary>
        /// Place the DigitalOptions order
        /// </summary>
        /// <param name="requestId">Request identifier<example>5f2c370145a047c7b87f2680556b3b93</example></param>
        /// <param name="pair">The Active pair, make sure your place with correct active.</param>
        /// <param name="direction">The position direction.</param>
        /// <param name="duration">The duration period in (1Min, 5Min, 15Min) from now</param>
        /// <param name="amount">The Amount of position</param>
        /// <returns></returns>
        Task<DigitalOptionsPlacedResult> PlaceDigitalOptions(string requestId, ActivePair pair, OrderDirection direction,
            DigitalOptionsExpiryDuration duration, double amount);

        /// <summary>
        /// Place the DigitalOptions order from the instruments_id
        /// </summary>
        /// <param name="requestId">Request identifier<example>5f2c370145a047c7b87f2680556b3b93</example></param>
        /// <param name="instrumentId">The Instrument identifier <example>doEURUSD201907191250PT5MPSPT</example></param>
        /// <param name="amount">The Amount of position</param>
        /// <returns></returns>
        Task<DigitalOptionsPlacedResult> PlaceDigitalOptions(string requestId, string instrumentId, double amount);

        #endregion

    }
}