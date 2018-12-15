using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WechatRobot.Entity;

namespace WechatRobot.Classes
{
    public class WeChatHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WeChatHelper()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <param name="appid">应用标识</param>
        /// <param name="secret">应用密钥</param>
        public WeChatHelper(string corpid, string corpsecret)
            : this()
        {
            _corpid = corpid;
            _corpsecret = corpsecret;
        }

        private string _corpid = "";

        /// <summary>
        /// 应用标识
        /// </summary>
        public string Corpid
        {
            get { return _corpid; }
            set { _corpid = value; }
        }

        private string _corpsecret = "";

        /// <summary>
        /// 应用密钥
        /// </summary>
        public string CorpSecret
        {
            get { return _corpsecret; }
            set { _corpsecret = value; }
        }

        private string _OpenID = "";
        /// <summary>
        /// openid
        /// </summary>
        public string OpenID
        {
            get { return _OpenID; }
            set { _OpenID = value; }
        }

        private AccessToken _accessToken = null;

        /// <summary>
        /// 获取当前或重新获取有效的AccessToken对象
        /// </summary>
        public AccessToken AccessToken
        {
            get
            {
                if (_accessToken == null)
                    _accessToken = GetAccessToken();
                else
                {
                    if (_accessToken.TimeOutDateTime <= DateTime.Now)
                        _accessToken = GetAccessToken();
                }
                return _accessToken;
            }
        }

        private AccessToken GetAccessToken()
        {
            AccessToken rst = null;
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", _corpid, _corpsecret);
            HttpHelper helper = new HttpHelper();
            HttpItem item = new HttpItem();
            item.URL = url;
            HttpResult result = helper.GetHtml(item);
            if (result.Html.Trim() != "")
                rst = JsonConvert.DeserializeObject<AccessToken>(result.Html.Trim());
            if (rst.Access_token.Trim() == "")
                rst = null;
            return rst;
        }

        /// <summary>
        /// 根据用户的OpenID来取得用户信息
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns>返回的Json</returns>
        public string GetUserBasicInfoByOpenID(string OpenID)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}", AccessToken.Access_token, OpenID);
            HttpHelper helper = new HttpHelper();
            HttpItem item = new HttpItem();
            item.URL = url;
            HttpResult result = helper.GetHtml(item);
            return result.Html;
        }


    }
}