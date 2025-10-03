namespace Tetris;

public class SaveFile
{
    string fileName;
    string path;
    
    public SaveFile(string fileName)
    {
        this.fileName = fileName;
        path = $"assets/{fileName}.txt";
    }

    public void Save(int value)
    {
        File.WriteAllText(path, value.ToString());
    }

    public int Load()
    {
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);
            
            if (int.TryParse(text, out int value)) return value;
        }
        
        return 0;
    }
}