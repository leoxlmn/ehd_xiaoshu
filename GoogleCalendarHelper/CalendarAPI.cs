using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Util;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Samples.Helper;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.Messaging;
using Google.Apis.Services;
using System.Globalization;
using System.Xml;


namespace GoogleCalendarHelper {
    public class CalendarAPI {

        const string STORAGE = "google.calendar.helper";
        const string KEY = "jdje29823"; //just a random key

        //Calendar scopes
        private IList<string> scopes = new List<string> { CalendarService.Scopes.Calendar.GetStringValue() };

        //Calendar service
        private CalendarService service = null;

        //Authentication callback
        private IAuthorizationState GetAuthorization(NativeApplicationClient client) {

            IAuthorizationState state = AuthorizationMgr.GetCachedRefreshToken(STORAGE, KEY);
            if (state != null) {
                try {
                    client.RefreshToken(state);
                    return state;
                } catch (ProtocolException ex) {
                    Console.WriteLine("Using an existing refresh token failed: " + ex.Message);
                    Console.WriteLine();
                }
            }

            state = AuthorizationMgr.RequestNativeAuthorization(client, scopes.ToArray());
            AuthorizationMgr.SetCachedRefreshToken(STORAGE, KEY, state);
            return state;
        }

        private void initiateService() {
            NativeApplicationClient provider = new NativeApplicationClient(GoogleAuthenticationServer.Description);
            provider.ClientIdentifier = @"348610301137.apps.googleusercontent.com";//credentials.ClientId;
            provider.ClientSecret = @"Lk6zMDm5JLhC9qF2VkleqeKD"; //credentials.ClientSecret;
            OAuth2Authenticator<NativeApplicationClient> auth = new OAuth2Authenticator<NativeApplicationClient>(provider, GetAuthorization);

            BaseClientService.Initializer initializer = new BaseClientService.Initializer();
            initializer.Authenticator = auth;
            service = new CalendarService(initializer);
        }

        #region Constructor
        public CalendarAPI() {
            initiateService();
        }
        #endregion

        /// <summary>
        /// Retrieve all the events for a given calendar and datetime range.
        /// </summary>
        /// <param name="CalendarId">CalendarId which usually is the gmail account</param>
        /// <param name="FromTime">Datetime range start</param>
        /// <param name="ToTime">Datetime range end</param>
        /// <returns></returns>
        public IList<Event> GetCalendarEvents(string CalendarId, DateTime? FromTime, DateTime? ToTime) {

            IList<Event> Events = null;

            CalendarListEntry calendar = service.CalendarList.Get(CalendarId).Execute();

            if (calendar != null) {
                EventsResource.ListRequest request = service.Events.List(calendar.Id);
                if (FromTime.HasValue) {
                    request.TimeMin = string.Format("{0:yyyy-MM-ddTHH:mm:ss-00:00}", FromTime.Value.ToUniversalTime());
                }
                if (ToTime.HasValue) {
                    request.TimeMax = string.Format("{0:yyyy-MM-ddTHH:mm:ss-00:00}", ToTime.Value.ToUniversalTime());
                }

                Events = request.Execute().Items;
            }

            return Events;
        }

        /// <summary>
        /// Try to add a calendar event for the given calendar
        /// </summary>
        /// <param name="CalendarId"></param>
        /// <param name="NewEvent"></param>
        public void AddCalendarEvent(string CalendarId, Event NewEvent) {

            if (CalendarId == null || CalendarId.Trim().Length <= 0 || NewEvent == null) return;

            service.Events.Insert(NewEvent, CalendarId).Execute();
        }

        /// <summary>
        /// Logoff from Google Calendar
        /// </summary>
        public void Logoff() {
            AuthorizationMgr.ClearRefreshToken(STORAGE);
            initiateService();
        }

        public DateTime? GetLocalTime(string rfc3339Time) {
            DateTime? ret = null;

            try {
                ret = XmlConvert.ToDateTime(rfc3339Time, XmlDateTimeSerializationMode.Local);
            } catch {
            }

            return ret;
        }
    }
}
