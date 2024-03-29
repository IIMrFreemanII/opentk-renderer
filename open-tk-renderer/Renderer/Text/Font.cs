using System.Collections;
using open_tk_renderer.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SharpFont;

namespace open_tk_renderer.Renderer.Text;

public class Font
{
  public string name;
  private readonly Dictionary<uint, Dictionary<char, Character>> _charactersBySize = new();

  private readonly Face _face;

  private Font(string path)
  {
    var absPath = PathUtils.FromLocal(path);
    string fileName = Path.GetFileName(absPath);
    name = fileName.Split('.').First();
    _face = new Face(FontsController.Library, absPath);
  }

  public static Font FromFile(string path)
  {
    var font = new Font(path);
    FontsController.Add(font);
    return font;
  }

  public Character GetCharBySize(char character, uint fontSize)
  {
    if (_charactersBySize.TryGetValue(fontSize, out var characters))
    {
      if (characters.TryGetValue(character, out var outChar))
      {
        return outChar;
      }

      var tempCharacter = CreateCharacterBySize(character, fontSize);
      characters.Add(character, tempCharacter);
      return tempCharacter;
    }

    var newChar = CreateCharacterBySize(character, fontSize);
    var newCharactersBySize = new Dictionary<char, Character>
      { { character, newChar } };
    _charactersBySize.Add(fontSize, newCharactersBySize);
    return newChar;
  }

  private Character CreateCharacterBySize(char character, uint size)
  {
    // set 1 byte pixel alignment 
    GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

    _face.SetPixelSizes(0, size);
    _face.LoadChar(
      character,
      LoadFlags.Render,
      LoadTarget.Normal
    );
    var glyph = _face.Glyph;
    var bitmap = glyph.Bitmap;

    // create glyph texture
    var texObj = GL.GenTexture();
    GL.BindTexture(TextureTarget.Texture2D, texObj);

    // set texture parameters
    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureMinFilter,
      (int)TextureMinFilter.Linear
    );
    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureMagFilter,
      (int)TextureMagFilter.Linear
    );
    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureWrapS,
      (int)TextureWrapMode.ClampToEdge
    );
    GL.TexParameter(
      TextureTarget.Texture2D,
      TextureParameterName.TextureWrapT,
      (int)TextureWrapMode.ClampToEdge
    );

    GL.TexImage2D(
      TextureTarget.Texture2D,
      0,
      PixelInternalFormat.R8,
      bitmap.Width,
      bitmap.Rows,
      0,
      PixelFormat.Red,
      PixelType.UnsignedByte,
      bitmap.Buffer
    );

    // bind default texture
    GL.BindTexture(TextureTarget.Texture2D, 0);
    // set default (4 byte) pixel alignment 
    GL.PixelStore(PixelStoreParameter.UnpackAlignment, 4);

    return new Character
    {
      TextureId = texObj,
      Size = new Vector2(bitmap.Width, bitmap.Rows),
      Bearing = new Vector2(glyph.BitmapLeft, glyph.BitmapTop),
      Advance = glyph.Advance.X.Value
    };
  }

  // private Dictionary<char, Character> CreateCharactersBySize(uint fontSize)
  // {
  //   Dictionary<char, Character> characters = new();
  //
  //   try
  //   {
  //     for (uint c = 0; c < 128; c++)
  //     {
  //       characters.Add((char)c, CreateCharacterBySize((char)c, fontSize));
  //     }
  //   }
  //   catch (Exception e)
  //   {
  //     Console.WriteLine(e);
  //   }
  //
  //   return characters;
  // }
}
