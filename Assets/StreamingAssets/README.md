# distributed-tcp-relay

## 概要

distributed-tcp-relayはTCP通信をリレーするためのプログラムです。  
一般家庭ではルーターを介しインターネットに利用しているため、家庭内ネットワークでサーバーを立てても、基本的にはネットワーク外のPCからはアクセスできません。
ネットワーク外からのアクセスを受け付けるためには、ルーターにポート開放などの設定が必要で、ネットワークの知識が少ない初心者には難しい操作になります。  
distributed-tcp-relayはlibp2pを用いることで、手動でのポート開放の設定を行うことなく、異なるネットワーク間での通信を実現するソフトウェアです。  

※すべてのTCP通信がリレーできるわけではありません。ネットワーク環境によっては通信が出来ない場合もあります。

## 使い方

![ネットワーク構成のサンプル](./image/overview.png)

### コマンドラインオプション

```
-sa Server Address      # relayがClient Modeとして起動するとき、コネクションを受け付けるサーバーアドレス
-ca Client Address      # relayがServer Modeとして起動するとき、コネクションを転送する先のサーバーアドレス
-r  Rendezvous String   # サーバーの通信を行う際に使用する共通の文字列
-s  Server Mode         # Server Modeで起動する
```

### Server Modeでの起動例

Serverを提供する側は、適当な合言葉(Randezvous)を決めてServer Modeとして起動してください。

```
# 自分のPCのlocalhost:7070で起動しているサーバーをリレーしている。
relay -s -ca localhost:7070 -r Randezvous
```

### Client Modeでの起動例

Client側は、先ほど決めた合言葉を設定し、Client Modeとして起動してください。

```
# 自分のPCのlocalhost:8080にサーバーが転送されています。
# クライアント側のソフトは、localhost:8080に接続するように設定してください。
relay -sa localhost:8080 -r Randezvous
```

