using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using UnityEditor;

public class CustomFontMaker : EditorWindow
{
//使用教程
//    string str = @"1-下载安装BMFont软件
//2-打开BMFont软件,Edit--Open Image Manager--Image--Import Image--导入0 ~9的字体png文件--设置每一
// 个图片的Id(比如0的ASCII码值是48, 那么就设置Id为48.1的Id为49),每个图片都要设置Id--参照ASCII码在线
//  转换.注意,图片不能放在中文路径下面,否则导入后不显示
//3-Edit--(Un)select all chars
//4-Options--Export Options--设置Texture下面的Bit depth为32位, File format的 Font descriptor为XML

// 并且贴图格式(Textures)改为png点击OK
// 注意:里面的贴图宽(Width)和高(Height)要根据图片的大小进行设置,如果太小的话就不行,预览的时候会提

// 示, 预览里面的素材不全, 导出来也不全, 要注意
//5-预览方式:Options--visualize
//6-导出fnt文件与贴图 Options--SaveBitmap font as, 写入文件名保存
//7-把生成的这两个文件放到Unity项目里面,贴图格式要改成Sprite(2D and UI)

//在Unity里面的操作
//7-创建一个自定义字体(Create- Custom font)
//8-创建一个材质球,把Shader选项改为GUI/Text Shader, 接着, 把BMFont生成的贴图拉到Texture框里面去;
//再把材质球拉进自定义字体的材质框(Default Material)里面
//9-把CustomFontMaker这个脚本放在Unity项目里面的Editor文件夹里面,菜单栏就会出现一个BMFontTools,点

//击子选项, 打开制作窗体
//8-把对应的BMFont生成的fnt文件还有自己创建的自定义字体拉进对应的框里面
//9-点击创建字体
//10-创建完成后,自定义字体就能用了
//注意:在画布下面创建一个Text组件,把我们创建好的自定义字体拖放进去就能显示了,但是不能修改字体大小

//的Size, 可以调整缩放来实现


//";
    //使用unity创建的自定义字体
    public Font font;
    //BMFont创建的fnt文件
    public TextAsset textAsset;

    [MenuItem("BMFontTools/ShowBMFontTools")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(CustomFontMaker));

    }
    void OnGUI()
    {
      
        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("使用unity创建的自定义字体(创建方法Create-Custom font", EditorStyles.boldLabel);
        font = (Font)EditorGUILayout.ObjectField("Unity Custom Font ", font, typeof(Font), false);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("使用 BMFont创建的fnt文件(注意,用BMFont生成时必须选择XML格式的)", EditorStyles.boldLabel);
        textAsset = (TextAsset)EditorGUILayout.ObjectField("BMFont fnt (.fnt)", textAsset, typeof(TextAsset), false);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("把unity创建的自定义字体制作完整---fnt写进自定义字体", EditorStyles.boldLabel);
        if (GUILayout.Button("创建字体"))
        {
            if (font == null) this.ShowNotification(new GUIContent("请选择unity创建的自定义字体"));
            if (textAsset == null) this.ShowNotification(new GUIContent("请选择BMFont创建的fnt文件"));
            CreateFont();
            this.ShowNotification(new GUIContent("创建成功"));
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("使用教程查看CustomFontMaker这个脚本的注释", EditorStyles.boldLabel);
      
        EditorGUILayout.EndVertical();
       
    }

    void OnInspectorUpdate()
    {
        this.Repaint();
    }


    void  CreateFont()
    {

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textAsset.text);


        int totalWidth = Convert.ToInt32(xmlDocument["font"]["common"].Attributes["scaleW"].InnerText);
        int totalHeight = Convert.ToInt32(xmlDocument["font"]["common"].Attributes["scaleH"].InnerText);

        XmlElement xml = xmlDocument["font"]["chars"];
        ArrayList characterInfoList = new ArrayList();


        for (int i = 0; i < xml.ChildNodes.Count; ++i)
        {
            XmlNode node = xml.ChildNodes[i];
            if (node.Attributes == null)
            {
                continue;
            }
            int index = Convert.ToInt32(node.Attributes["id"].InnerText);
            int x = Convert.ToInt32(node.Attributes["x"].InnerText);
            int y = Convert.ToInt32(node.Attributes["y"].InnerText);
            int width = Convert.ToInt32(node.Attributes["width"].InnerText);
            int height = Convert.ToInt32(node.Attributes["height"].InnerText);
            int xOffset = Convert.ToInt32(node.Attributes["xoffset"].InnerText);
            int yOffset = Convert.ToInt32(node.Attributes["yoffset"].InnerText);
            int xAdvance = Convert.ToInt32(node.Attributes["xadvance"].InnerText);

            CharacterInfo info = new CharacterInfo();
            Rect uv = new Rect();
            uv.x = (float)x / totalWidth;
            uv.y = (float)(totalHeight - y - height) / totalHeight;
            uv.width = (float)width / totalWidth;
            uv.height = (float)height / totalHeight;


            info.index = index;
            info.uvBottomLeft = new Vector2(uv.xMin, uv.yMin);
            info.uvBottomRight = new Vector2(uv.xMax, uv.yMin);
            info.uvTopLeft = new Vector2(uv.xMin, uv.yMax);
            info.uvTopRight = new Vector2(uv.xMax, uv.yMax);
            info.minX = xOffset;
            info.maxX = xOffset + width;
            info.minY = -yOffset - height;
            info.maxY = -yOffset;
            info.advance = xAdvance;
            info.glyphWidth = width;
            info.glyphHeight = height;


            characterInfoList.Add(info);
        }
        font.characterInfo = characterInfoList.ToArray(typeof(CharacterInfo)) as CharacterInfo[];

    }
}