Swagger 是一个规范和完整的框架，用于生成、描述、调用和可视化RESTful风格的 Web 服务。

### 认证和授权、Json Web Token

参考信息：[jwt官网](https://jwt.io/introduction/)

认证（Authentication）和授权（Authorization）的区别：

认证是对访问身份进行确认，如验证用户名和密码，而授权是在认证之后，判断是否具有权限进行某操作，如 Authorize 属性。简单说，他们之间先后顺序是先认证，再授权。

JSON Web令牌（JWT）是一个开放标准（[RFC 7519](https://tools.ietf.org/html/rfc7519)），它定义了一种紧凑且自包含的方式，用于在各方之间安全地将信息作为JSON对象传输。

维基百科定义：JSON WEB Token（JWT），是一种基于JSON的、用于在网络上声明某种主张的令牌（token）。JWT通常由三部分组成：头信息（header），消息体（payload）和签名（signature）。

以下是JSON Web  Token有用的一些情况：

- **授权**：这是使用JWT的最常见方案。一旦用户登录，每个后续请求将包括JWT，从而允许用户访问该令牌允许的路由，服务和资源。单一登录是当今广泛使用JWT的一项功能，因为它的开销很小并且可以在不同的域中轻松使用。
- **信息交换**：JSON Web Token是在各方之间安全地传输信息的好方法。因为可以对JWT进行签名（例如，使用公钥/私钥对），所以您可以确定发件人是他们所说的人。此外，由于签名是使用标头和有效负载计算的，因此您还可以验证内容是否遭到篡改。

JWT通常由三部分组成: 头信息（header）, 消息体（payload）和签名（signature）。

JWT通常如下所示：xxxxx.yyyyy.zzzzz

#### 头信息（header）

header通常由两部分组成：令牌的类型（即JWT）和所使用的签名算法，例如HMAC SHA256或RSA。

```json
{
  "alg": "HS256",//指定生成签名的算法，例如HMAC SHA256 or RSA
  "typ": "JWT"//Token的类型JWT
}
```

#### 消息体（payload）

各种声明（claimis）：

```json
{
  "sub": "1234567890",
  "name": "John Doe",
  "admin": true
}
```

可以包含非敏感的信息,不要包含敏感的信息如密码等

#### 签名（signature）

头信息base64编码+.+消息体base64编码+.+签名,签名是头信息base64编码.消息体base64编码加密而成

#### 完成的jwt

头信息base64编码+.+消息体base64编码+.+签名

看起来像这样:eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJsb2dnZWRJbkFzIjoiYWRtaW4iLCJpYXQiOjE0MjI3Nzk2Mzh9.gzSraSYS8EXBxLN_oWnFSRgCzcmJmMjLiuyu5CSpyHI