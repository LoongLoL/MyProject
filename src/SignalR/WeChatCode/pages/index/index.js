//index.js
//获取应用实例
const app = getApp()

Page({
  data: {
    motto: 'Hello World',
    userInfo: {},
    hasUserInfo: false,
    canIUse: wx.canIUse('button.open-type.getUserInfo'),
    wxCode: "未获取"
  },
  //事件处理函数
  bindViewTap: function () {
    wx.navigateTo({
      url: '../signalr/signalr'
    })
  },
  onLoad: function () {
    wx.login({
      success: res => {
        this.setData({
          wxCode: res.code,
        })
        console.log(res.code);
      }
    });
    if (app.globalData.userInfo) {
      this.setData({
        userInfo: app.globalData.userInfo,
        hasUserInfo: true
      })
    } else if (this.data.canIUse) {
      // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
      // 所以此处加入 callback 以防止这种情况
      app.userInfoReadyCallback = res => {
        this.setData({
          userInfo: res.userInfo,
          hasUserInfo: true
        })
      }
    } else {
      // 在没有 open-type=getUserInfo 版本的兼容处理
      wx.getUserInfo({
        success: res => {
          app.globalData.userInfo = res.userInfo
          this.setData({
            userInfo: res.userInfo,
            hasUserInfo: true
          })
        }
      })

    }
  },
  getUserInfo: function (e) {
    console.log(e)
    app.globalData.userInfo = e.detail.userInfo
    this.setData({
      userInfo: e.detail.userInfo,
      hasUserInfo: true
    })
  },
  login: function () {
    return new Promise(function (resolve, reject) {
      wx.login({
        success: function (res) {
          if (res.code) {
            resolve(res.code);
          } else {
            reject(res);
          }
        },
        fail: function (err) {
          reject(err);
        }
      });
    });
  },

  loginByWeixin: function () {
    let code = null;
    return new Promise(function (resolve, reject) {
      return util.login().then((res) => {
        code = res.code;
        return util.getUserInfo();
      }).then((userInfo) => {
        //登录远程服务器
        util.request(api.AuthLoginByWeixin, {
          code: code,
          userInfo: userInfo
        }, 'POST').then(res => {
          if (res.errno === 0) {
            //存储用户信息
            wx.setStorageSync('userInfo', res.data.userInfo);
            wx.setStorageSync('token', res.data.token);

            resolve(res);
          } else {
            reject(res);
          }
        }).catch((err) => {
          reject(err);
        });
      }).catch((err) => {
        reject(err);
      })
    });
  }
})