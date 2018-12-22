# XgPush.SDK.Server
[![NuGet version (XgPush.SDK.Server)](https://img.shields.io/nuget/v/XgPush.SDK.Server.svg)](https://www.nuget.org/packages/XgPush.SDK.Server/)

XgPush Server SDK third party (unofficial) 

## 快速开始

* Package Manager

```
Install-Package XgPush.SDK.Server
```

* .NET CLI

```
dotnet add package XgPush.SDK.Server
```

### 初始化SDK

#### AspNetCore 1.x/2.x

[YourProject\Program.cs](/samples/aspnetcore21(netcoreapp21)/Program.cs)
```
public static void Main(string[] args){
    + XgPush.SDK.Server.XingePushClient.InitHttpClientCompatDefaultMagic();
}
```

#### AspNet

[YourProject\Global.asax.cs](/samples/aspnetmvc5(net45)/Global.asax.cs)
```
protected void Application_Start(){
    + XgPush.SDK.Server.XingePushClient.InitHttpClientCompatDefaultMagic();
}
```

## 支持的目标框架

* .NET Standard 1.3
* .NET Standard 2.0
* .NET Framework 4.0
* .NET Framework 4.5

## 示例

* Push API v3
    * 全量推送
    * 标签推送
    * 单设备推送
    * 设备列表推送
    * 单账号推送
    * 账号列表推送
* Rest API v2
    * 全量推送
    * 群推送
    * 单推
    * 查询消息状态
    * 取消推送
    * 批量新增标签
    * 批量删除标签
    * 查询全部标签
    * 查询单个指定设备的标签
    * 查询单个指定标签的Token总数
    * 查询单个账号关联的设备
    * 删除单个账号关联的单个设备Token
    * 删除单个账号关联的全部设备Token
    * 查询应用覆盖的设备Token总数
    * 查询指定设备Token的注册状态

[SampleController](https://github.com/RMBGame/XgPush.SDK.Server/tree/master/samples/aspnetcore21(netcoreapp21)/Controllers/SampleController.cs)

### [Push API v3](https://xg.qq.com/docs/server_api/v3/push_api_v3.html)

#### 全量推送

```
var msg_android = new Message
{
    Title = "your title",
    Content = "your content",
};

var req_push_all = new PushRequest<Message>.All
{
    Message = msg_android,
};

var rsp_push_all = await pushClient.v3.Push(req_push_all);
```

#### 标签推送

```
var tags = new[] { "tag1", "tag2", "tag3" };

var req_push_tag = new PushRequest<Message>.Tag
{
    Message = msg_android,
    Value = new TagList(tags)
    {
        Operators = Operator.OR // Operator.AND
    },
};

var rsp_push_tag = await pushClient.v3.Push(req_push_tag);
```

#### 单设备推送

```
var req_push_token = new PushRequest<Message>.Token
{
    Message = msg_android,
    Value = "token1",
};

var rsp_push_token = await pushClient.v3.Push(req_push_token);
```

#### 设备列表推送

```
var tokens = new[] { "token1", "token2", "token3" };

var req_push_tokens = new PushRequest<Message>.TokenList
{
    Message = msg_android,
    Values = tokens,
};

var rsp_push_tokens = await pushClient.v3.Push(req_push_tokens);
```

#### 单账号推送

```
var req_push_account = new PushRequest<Message>.Account
{
    Message = msg_android,
    Value = "account1",
    Type = 0, //  往单个账号的最新的device上推送信息
    // AccountType = AccountType.PhoneNumber, // 账号类型(可选)
};

var rsp_push_account = await pushClient.v3.Push(req_push_account);
```

#### 账号列表推送

```
var accounts = new[] { "account1", "account2", "account3" };

var req_push_accounts = new PushRequest<Message>.AccountList
{
    Message = msg_android,
    Values = accounts,
    //PushId = "123",
};

var rsp_push_accounts = await pushClient.v3.Push(req_push_accounts);
```

### [Rest API v2](https://xg.qq.com/docs/server_api/v2/rest.html)

#### 查询消息状态

```
var pushIds = new[] { "push_id_1", "push_id_2" };

var req_query_push_status = await pushClient.QueryPushStatus(pushIds);
```

#### 取消推送


```
var req_cancel_push = await pushClient.CancelTimingPush("push_id_1");
```