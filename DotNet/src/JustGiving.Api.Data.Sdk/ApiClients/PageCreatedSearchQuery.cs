using System;
using System.Text;
using System.Web;

namespace GG.Api.Services.Data.Sdk.ApiClients
{
    ///<summary>
    /// Encapsulates a query for the Pages Created report
    ///</summary>
    public class PageCreatedSearchQuery
    {
        public string EventCustomCode1 { get; set; }
        public string EventCustomCode2 { get; set; }
        public string EventCustomCode3 { get; set; }
        public string PageCustomCode1 { get; set; }
        public string PageCustomCode2 { get; set; }
        public string PageCustomCode3 { get; set; }
        public string PageCustomCode4 { get; set; }
        public string PageCustomCode5 { get; set; }
        public string PageCustomCode6 { get; set; }
        public bool? IsActivePage { get; set; }
        public string AppealName { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? PageExpiresBefore { get; set; }
        public DateTime? PageExpiresAfter { get; set; }
        public DateTime? EventBefore { get; set; }
        public DateTime? EventAfter { get; set; }

        public string GetQueryStringPairs()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(EventCustomCode1))
            {
                sb.AppendFormat("eventcc1={0}", HttpUtility.UrlEncode(EventCustomCode1));
            }

            if (!string.IsNullOrEmpty(EventCustomCode2))
            {
                sb.AppendFormat("&eventcc2={0}", HttpUtility.UrlEncode(EventCustomCode2));
            }

            if (!string.IsNullOrEmpty(EventCustomCode3))
            {
                sb.AppendFormat("&eventcc3={0}", HttpUtility.UrlEncode(EventCustomCode3));
            }

            if (!string.IsNullOrEmpty(PageCustomCode1))
            {
                sb.AppendFormat("&pagecc1={0}", HttpUtility.UrlEncode(PageCustomCode1));
            }

            if (!string.IsNullOrEmpty(PageCustomCode2))
            {
                sb.AppendFormat("&pagecc2={0}", HttpUtility.UrlEncode(PageCustomCode2));
            }

            if (!string.IsNullOrEmpty(PageCustomCode3))
            {
                sb.AppendFormat("&pagecc3={0}", HttpUtility.UrlEncode(PageCustomCode3));
            }

            if (!string.IsNullOrEmpty(PageCustomCode4))
            {
                sb.AppendFormat("&pagecc4={0}", HttpUtility.UrlEncode(PageCustomCode4));
            }

            if (!string.IsNullOrEmpty(PageCustomCode5))
            {
                sb.AppendFormat("&pagecc5={0}", HttpUtility.UrlEncode(PageCustomCode5));
            }

            if (!string.IsNullOrEmpty(PageCustomCode6))
            {
                sb.AppendFormat("&pagecc6={0}", HttpUtility.UrlEncode(PageCustomCode6));
            }

            if(IsActivePage.HasValue)
            {
                sb.AppendFormat("&active={0}", IsActivePage.Value);
            }

            if (!string.IsNullOrEmpty(AppealName))
            {
                sb.AppendFormat("&appeal={0}", HttpUtility.UrlEncode(AppealName));
            }

            if(CompanyId.HasValue)
            {
                sb.AppendFormat("&company={0}", CompanyId.Value);
            }

            if (PageExpiresAfter.HasValue)
            {
                sb.AppendFormat("&expiresafter={0:yyyy-MM-dd}", PageExpiresAfter.Value);
            }

            if (PageExpiresBefore.HasValue)
            {
                sb.AppendFormat("&expiresbefore={0:yyyy-MM-dd}", PageExpiresBefore.Value);
            }

            if (EventAfter.HasValue)
            {
                sb.AppendFormat("&eventafter={0:yyyy-MM-dd}", EventAfter.Value);
            }

            if (EventBefore.HasValue)
            {
                sb.AppendFormat("&eventbefore={0:yyyy-MM-dd}", EventBefore.Value);
            }

            return sb.ToString();
        }
    }
}