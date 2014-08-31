# KanColleLib用イベント周りのジェネレータ

# 入力データは以下のとおり
# kcsapiurl,イベント名,リクエストクラス,レスポンスクラス
# リクエストクラスは初期がTransmissionRequest.RequestBase
# api_dataが必要でなければレスポンスクラスはobjectにする

# 行頭が#で始まる行は無視する

# イベント定義は以下
# /// <summary>
# /// [kcsapiurl] を受信して解析に成功した際に呼び出されます
# /// </summary>
# public event Get[イベント名]EventHandler Get[イベント名];
# public delegate void Get[イベント名]EventHandler(object sender, [リクエストクラス] request, Svdata<[レスポンスクラス]> response);
# protected virtual void OnGet[イベント名]([リクエストクラス] request, Svdata<[レスポンスクラス]> response) { if (Get[イベント名] != null) Get[イベント名](this, request, response); }

# イベント実装は以下
# api_dataが必要な場合
# case "[kcsapiurl]":
# if(json.api_data())
#   OnGet[イベント名](new [リクエストクラス](request), Svdata<[レスポンスクラス]>.fromDynamic(json, [レスポンスクラス].fromDynamic(json.api_data)));
# else
#   throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
# そうでない場合
# case "[kcsapiurl]":
# OnGet[イベント名](new [リクエストクラス](request), Svdata<object>.fromDynamic(json, null));

File.open("event.txt", "w"){|file| }

File.open("switch.txt", "w"){|file| }

open( "source.txt" ) {|f|
	while line = f.gets
		if line[0] == '#'
			next
		end

		arr = line.strip.split(',')

		kcsapiurl = arr[0]
		eventname = arr[1]
		requestclass = arr[2]
		responseclass = arr[3]
		flag = (arr[3] != "object")

		File.open("event.txt", "a"){|file|
			file.puts ""
			file.puts "/// <summary>"
			file.puts "/// " + kcsapiurl + " を受信して解析に成功した際に呼び出されます"
			file.puts "/// <summary>"
			file.puts "public event Get" + eventname + "EventHandler Get" + eventname + ";"
			file.puts "public delegate void Get" + eventname + "EventHandler(object sender, " + requestclass + " request, Svdata<" + responseclass + "> response);"
			file.puts "protected virtual void OnGet" + eventname + "(" + requestclass + " request, Svdata<" + responseclass + "> response) { if (Get" + eventname + " != null) Get" + eventname + "(this, request, response); }"
		}

		File.open("switch.txt", "a"){|file|
			file.puts "case \"" + kcsapiurl + "\":"
			if flag
				file.puts "if(json.api_data())"
				file.puts "OnGet" + eventname + "(new " + requestclass + "(request), Svdata<" + responseclass + ">.fromDynamic(json, " + responseclass + ".fromDynamic(json.api_data)));"
				file.puts "else"
				file.puts "throw new KanColleLibException(string.Format(\"No api_data: {0}\", kcsapiurl));"
			else
				file.puts "OnGet" + eventname + "(new " + requestclass + "(request), Svdata<" + responseclass + ">.fromDynamic(json, null));"
			end
			file.puts "break;"
		}
	end
}