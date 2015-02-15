# 艦これクライアント「みおつくし」Ver.2

「みおつくし」は、DMMによるWebゲーム『艦隊これくしょん -艦これ-』の補助ツールです。

身内のみで配布していた、みおつくし Ver.0を、パフォーマンス向上を図るため、すべて書き換えることにいたしました。

2015年夏リリース予定。

## 実装機能（予定）

* 艦隊・各艦の状況表示
 * 合計レベル、ドラム缶数
 * 燃料消費
 * 制空値
 * 2-5索敵値
 * 対空カットインや連撃・カットインの可否
* 遠征・任務・入渠ドック・工廠ドックなどの状況表示
* 戦闘解析・勝利予測・敵艦情報
* 艦娘・装備一覧
* 図鑑・遠征などのスタティックな情報

## 動作環境

* Windows Vista以降（Windows XPでは動作しません）
* .NET Framework 4.5.1

## 開発環境

* Windows 8 Pro x64
* Visual Studio Professional 2013

## 使用ライブラリ

* [FiddlerCore](http://fiddler2.com/fiddlercore)（通信のキャプチャ）
* [DynamicJson](http://dynamicjson.codeplex.com/)（Jsonのデコード） - Microsoft Public License
* [Prism](https://compositewpf.codeplex.com/)（MVVM インフラストラクチャ） - Apache License 2.0
* [MahApps.Metro](http://mahapps.com/)（メトロデザインのベース） - Microsoft Public License

## 適用ライセンス

このソフトウェアは、MITライセンスのもとで公開されています。
詳細な内容に関してはLICENSE.TXTをご覧ください。
