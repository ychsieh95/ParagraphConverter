# ParagraphConverter

一個能將 PDF 複製出的零碎字句轉換為完整段落的小工具。

## Supports

Windows 7sp1/8/8.1/10 with [.NET Framework 4.6.1](https://www.microsoft.com/zh-tw/download/details.aspx?id=49982) (or higher)

## Function

* 選單列
    * Settings
        * Listen Clipboard\
          （選項）是否監聽剪貼簿，當剪貼簿文字變動時自動貼上剪貼簿文字至程式
        * Append Text Mode\
          （選項）是否啟用添加文字方式複製，若不啟用則為取代模式
        * Auto Convert\
          （選項）是否自動轉換段落
        * Auto Copy To Clipboard\
          （選項）是否自動將轉換段落後之結果複製至剪貼簿
        * Exit\
          （操作）結束程式
* 主畫面
    * Convert Paragraph\
      （操作）轉換為段落
    * Copy To Clipboard\
      （操作）將程式內文字複製至剪貼簿
    * Clear Clipboard\
      （操作）清除剪貼簿內容

## How To Use

* 將 PDF 複製出的零碎字句轉換為完整段落
    1. 複製 PDF 文字並貼入程式
    2. 按下 `Convert` 完成轉換
* 監聽剪貼簿，當剪貼簿文字變動時自動貼上剪貼簿文字至程式
    1. 按下 `Settings`
    2. 勾選 `Listen Clipboard` 即可開啟監視功能
    3. 若欲取消，取消勾選 `Listen Clipboard` 即可
* 監聽剪貼簿，複製文字後自動轉換
    1. 按下 `Settings`
    2. 勾選 `Listen Clipboard`
	3. 勾選 `Auto Convert`
* 監聽剪貼簿，複製文字後自動轉換並存入剪貼簿
    1. 按下 `Settings`
    2. 勾選 `Listen Clipboard`
	3. 勾選 `Auto Convert`、`Auto Copy To Clipboard`
* 監聽剪貼簿，複製文字後自動添加至現有文字並轉換後存入剪貼簿
    1. 按下 `Settings`
    2. 勾選 `Listen Clipboard`
	3. 勾選 `Append Text Mode`、`Auto Convert`、`Auto Copy To Clipboard`

## Example

| Brfore                                   | After                                   |
| :--------------------------------------: | :-------------------------------------: |
| ![Brfore](https://imgur.com/7KfWXSj.png) | ![After](https://imgur.com/NSdEpln.png) |

## References

Images: from [iconarchive.com](http://www.iconarchive.com/)