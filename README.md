# unity-oculus-integration-sandbox

[プログマのプログラム日記](https://rhikos-prgm.hatenablog.com/)に書いたOculus integration関連記事のコードを共有するためのリポジトリです。

## 収録シーン

### Unity + Oculus Integrationで物をつかむ

Unity + Oculus Integrationを使ってコントローラやハンドトラッキングでオブジェクトをつかむまでの手順をStep by stepで説明します。  
やっていることはOculus Integrationのサンプルシーン「HandGrabExamples」と同様ですが、これを0から組み立ててみてSceneを構成するコンポーネントの役割への理解を深めようという趣旨の記事です。  

- Assets/Sandbox/HandGrab/Scenes/Scene0  
[その１：ルームスケールで動き回れるSceneを作成](https://rhikos-prgm.hatenablog.com/entry/2022/10/02/084517)
- Assets/Sandbox/HandGrab/Scenes/Scene1  
[その２：コントローラを表示](https://rhikos-prgm.hatenablog.com/entry/2022/10/02/223800)
- Assets/Sandbox/HandGrab/Scenes/Scene2  
[その３：手を表示](https://rhikos-prgm.hatenablog.com/entry/2022/10/03/113200)
- Assets/Sandbox/HandGrab/Scenes/Scene3  
[その４：オブジェクトをつかむ](https://rhikos-prgm.hatenablog.com/entry/2022/10/04/055620)
- Assets/Sandbox/HandGrab/Scenes/Scene4  
[その５：つかむときの手のポーズを設定](https://rhikos-prgm.hatenablog.com/entry/2022/10/04/140627)

### Unity + Oculus Integrationでボタンを押す

Unity + Oculus Integrationを使ってコントローラやハンドトラッキングでボタンを押すまでの手順をStep by stepで説明します。  
やっていることはOculus Integrationのサンプルシーン「PokeExamples」と同様ですが、これを0から組み立ててみてSceneを構成するコンポーネントの役割への理解を深めようという趣旨の記事です。  

- Assets/Sandbox/Poke/Scenes/PokeScene0  
[その1：無地のボタン](https://rhikos-prgm.hatenablog.com/entry/2022/10/14/153846)
- Assets/Sandbox/Poke/Scenes/PokeScene1  
[その２：ボタンの大きさ・高さの調整](https://rhikos-prgm.hatenablog.com/entry/2022/10/15/084824)
- Assets/Sandbox/Poke/Scenes/PokeScene2  
[その３：ラベル付きボタンの作成](https://rhikos-prgm.hatenablog.com/entry/2022/10/17/180516)
- Assets/Sandbox/Poke/Scenes/PokeScene3  
[その４：好きな形のボタンを作成](https://rhikos-prgm.hatenablog.com/entry/2022/10/18/135359)
- Assets/Sandbox/Poke/Scenes/PokeScene4  
[その５：Canvas上にボタンを作成](https://rhikos-prgm.hatenablog.com/entry/2022/10/28/172127)

### Unity + Oculus Integrationで仮想手首ボタンの実装
- Assets/Sandbox/WristUI/Scenes/WristUICanvas  
[Unity + Oculus Integrationで仮想手首ボタンの実装](https://rhikos-prgm.hatenablog.com/entry/2022/11/07/171933)

## 使い方

本リポジトリのシーンを実行するための手順を説明します。  

1. ソースコードをクローンする  
本リポジトリのソースコードをクローンしてください。
2. Oculus Integrationをインポートする  
Oculus Integrationを以下からダウンロードしてインポートしてください。  
https://developer.oculus.com/downloads/package/unity-integration/41.0  
※v44以降を使用した場合、Scene内の一部オブジェクト間で参照が切れてしまう現象が発生します。  
[こちら](https://rhikos-prgm.hatenablog.com/entry/2022/10/01/043120)の記事の「Oculus Integrationのインポート」あたりを参考にOculus Integrationをインポートしてください。






