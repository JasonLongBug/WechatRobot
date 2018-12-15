using System;

namespace WechatRobot.Entity
{
    /// <summary>
    /// 全局AccessToken
    /// </summary>
    public class AccessToken
    {
        public AccessToken()
        {
            _refreshDateTime = DateTime.Now;
        }

        private string _access_token = "";

        /// <summary>
        /// 全局控制Token
        /// </summary>
        public string Access_token
        {
            get
            {
                return _access_token;
            }
            set
            {
                _access_token = value;
            }
        }


        private int _expires_in = 0;

        /// <summary>
        /// 过期时长，以秒为单位
        /// </summary>
        public int Expires_in
        {
            get
            {
                return _expires_in;
            }
            set
            {
                _expires_in = value;
            }
        }

        private DateTime _refreshDateTime;

        /// <summary>
        /// 上次刷新时刻
        /// </summary>
        public DateTime RefreshDateTime
        {
            get
            {
                return _refreshDateTime;
            }
            set
            {
                _refreshDateTime = value;
            }
        }

        /// <summary>
        /// Token过期时间
        /// </summary>
        public DateTime TimeOutDateTime
        {
            get
            {
                return RefreshDateTime.AddSeconds(Expires_in - 1);
            }
        }
    }
}
