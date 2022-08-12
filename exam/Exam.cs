using filesystem;
using System.Text.RegularExpressions;
public class Exam
{
    public static IFileSystem CreateFileSystem()
    {
        // Devuelva aquí su instancia de IFileSystem
        var root = new Folder("/");

        return new FileSystem(root);
    }

    // Borre esta excepción y ponga su nombre como string, e.j.
    // Nombre => "Fulano Pérez Pérez";
    public static string Nombre => "Francisco Vicente Suarez Bellon";

    // Borre esta excepción y ponga su grupo como string, e.j.
    // Grupo => "C2XX";
    public static string Grupo => "C212";
}


#region Archivo
public class File : IFile, IEquatable<File>
{
    public int Size { set; get; }

    public string Name { set; get; }

    public File(string Name, int Size)
    {
        this.Name = Name;
        this.Size = Size;
    }

    public void ChangeFileValue(int value)
    {
        this.Size = value;
    }

    public bool Equals(File? other)
    {
        if (other == null)
        {
            throw new Exception();
        }
        return this.Name.Equals(other.Name);
    }
}

public class IFileComparer : IComparer<File>
{
    public int Compare(File? x, File? y)
    {
        return x.Name.CompareTo(y.Name);
    }
}

#endregion
#region Carpeta


public class Folder : IFolder
{

    public Folder Father { get; set; }
    public string Name { get; set; }

    public List<File> files { get; set; }

    public List<Folder> childrenFolders { get; set; }
    public Folder(string Name)
    {
        this.Name = Name;
        this.files = new List<File>();
        this.childrenFolders = new List<Folder>();
    }

    public void addFather(Folder father)
    {
        this.Father = father;
    }

    public void Remove()
    {
        this.Father.RemoveChild(this);
    }

    public void RemoveChild(Folder x)
    {
        for (int i = 0; i < this.childrenFolders.Count; i++)
        {

            if (childrenFolders[i].Name == x.Name)
            {
                childrenFolders.RemoveAt(i);
            }
        }
    }
    public void AddChild(Folder child)
    {
        child.addFather(this);
        this.childrenFolders.Add(child);

    }

    public IFile CreateFile(string name, int size)
    {
        foreach (var files in this.files)
        {
            if (files.Name == name) { throw new Exception(); }
        }
        File file = new File(name, size);

        this.files.Add(file);
        return file;
    }


    public void Change(Folder x, Folder d)
    {
        for (int i = 0; i < this.childrenFolders.Count; i++)
        {
            if (this.childrenFolders[i].Name == x.Name)
            {

            }
        }
    }


    public IFolder CreateFolder(string name)
    {

        foreach (var Childs in this.childrenFolders)
        {
            if (Childs.Name == name) { throw new Exception(); }
        }
        Folder folder = new Folder(name);
        this.childrenFolders.Add(folder);
        folder.addFather(this);

        return folder;
    }


    public void ChangeFileValue(string file, int newvalue)
    {
        foreach (var item in this.files)
        {
            if (item.Name == file)
            {
                item.Size = newvalue;
            }
        }
    }
    public IEnumerable<File> GetFilesSInInterfaz()
    {
        this.files.Sort(new IFileComparer());
        foreach (var item in this.files)
        {
            yield return item;
        }

    }

    public IEnumerable<IFile> GetFiles()
    {
        this.files.Sort(new IFileComparer());
        foreach (var item in this.files)
        {
            yield return item;
        }

    }

    public IEnumerable<Folder> GetFoldersSinInterfaz()
    {
        this.childrenFolders.Sort(new IFolderComparer());
        foreach (var item in this.childrenFolders)
        {
            yield return item;
        }
    }

    public IEnumerable<IFolder> GetFolders()
    {
        this.childrenFolders.Sort(new IFolderComparer());
        foreach (var item in this.childrenFolders)
        {
            yield return item;
        }
    }



    public int TotalSize()
    {
        int Provisional = 0;
        foreach (var item in this.files)
        {
            Provisional += item.Size;
        }

        foreach (var child in this.childrenFolders)
        {
            Provisional += child.TotalSize();
        }
        return Provisional;
    }
}


public class IFolderComparer : IComparer<Folder>
{
    public int Compare(Folder? x, Folder? y)
    {
        return x.Name.CompareTo(y.Name);
    }


}
#endregion


#region AdmiArchivos

public class FileSystem : IFileSystem
{

    protected Folder RootNode { get; set; }

    public FileSystem(Folder rootNode)
    {
        this.RootNode = rootNode;
    }
    public void Copy(string origin, string destination)
    {
        if (origin.Length == 1 && origin[0] == '/')
        {
            throw new Exception();
        }
        var IsFile = GetFileSinInterfaz(origin);
        var Destino = GetFOlderSInInterfaz(destination);

        if (IsFile != null)
        {
            if (!COmprobarArchivoEnDestino(IsFile, Destino))
            {
                Destino.CreateFile(IsFile.Name, IsFile.Size);

            }
            else
            {
                foreach (var item in Destino.GetFilesSInInterfaz())
                {

                    if (item.Name == IsFile.Name)
                    {
                        item.ChangeFileValue(IsFile.Size);
                    }
                }
            }

        }
        else if (GetFOlderSInInterfaz(origin) != null)
        {
            var IsFolder = GetFOlderSInInterfaz(origin);
            if (IsFolder.Name == "/") { throw new Exception(); }
            if (!ComprobarNOExistirDosCarpetasEnLAMisma(IsFolder, Destino))
            {

                var mm = Blended.AddFolders(IsFolder);
                Destino.AddChild(mm);
            }
            else
            {
                var xz = Destino.Father;
                xz.RemoveChild(Destino);
                var z = Blended.Blendeed(Destino, IsFolder);
                xz.AddChild(z);

            }
        }
        else
        {
            throw new Exception();
        }
    }


    private bool ComprobarNOExistirDosCarpetasEnLAMisma(Folder origin, Folder destino)
    {
        foreach (var item in destino.childrenFolders)
        {
            if (item.Name == origin.Name) return true;
        }
        return false;
    }

    protected bool COmprobarArchivoEnDestino(File file, Folder destino)
    {
        foreach (var item in destino.GetFiles())
        {
            if (file.Name == item.Name) return true;
        }
        return false;
    }
    public void Delete(string path)
    {
        if (path.Length == 1 && path[0] == '/')
        {
            throw new Exception();
        }

        var Isfile = GetFileSinInterfaz(path); //Buscar si es Archivo

        if (Isfile != null)
        {
            var direccion = this.Path(path);
            int count = direccion.Count;
            if (count > 0)
            {
                var fileName = direccion[count - 1];
                direccion.RemoveAt(count - 1);
                var Queue = new Queue<string>();
                foreach (var item in direccion)
                {
                    Queue.Enqueue(item);
                }
                var folderDestino = GetTheSearch(Queue);
                folderDestino.files.Remove(Isfile);
            }
        }
        else if (GetFOlderSInInterfaz(path) != null)
        {
            var IsFolder = GetFOlderSInInterfaz(path); //Buscar si es carpeta
            if (IsFolder.Name == "/") { throw new Exception(); }
            IsFolder.Remove();
        }
        else
        {
            throw new Exception();
        }

    }

    public IEnumerable<IFile> Find(FileFilter filter)
    {
        foreach (var Child in Recorrer.PreOrden(this.RootNode))
        {
            foreach (var file in Child.GetFiles())
            {
                if (filter(file)) { yield return file; }
            }
        }
    }


    public File GetFileSinInterfaz(string path)
    {
        var List = Path(path);
        int count = List.Count;
        if (count == 1) { return ReturnFromThisFolder(this.RootNode, List[0]); }
        var last = List[count - 1];
        List.RemoveAt(count - 1);
        var Queue = new Queue<string>();
        foreach (var item in List)
        {
            Queue.Enqueue(item);
        }

        var folder = GetTheSearch(Queue);

        return ReturnFromThisFolder(folder, last);
    }

    public IFile GetFile(string path)
    {
        var x = GetFileSinInterfaz(path);
        if (x != null) return x;
        throw new Exception();
    }

    private File ReturnFromThisFolder(Folder node, string name)
    {
        foreach (var item in node.GetFilesSInInterfaz())
        {
            if (item.Name == name) return item;
        }
        return null!;
    }

    private Folder GetFOlderSInInterfaz(string path)
    {
        if (path == this.RootNode.Name) { return this.RootNode; }
        var List = Path(path);
        var Queue = new Queue<string>();
        foreach (var item in List)
        {
            Queue.Enqueue(item);
        }
        return GetTheSearch(Queue);
    }
    public IFolder GetFolder(string path)
    {
        var x = GetFOlderSInInterfaz(path);
        if (x != null) return x;
        throw new Exception();
    }
    protected Folder GetTheSearch(Queue<string> path)
    {
        Queue<IFolder> folders = new Queue<IFolder>();

        var result = this.RootNode;

        folders.Enqueue(this.RootNode);
        while (folders.Count > 0 && path.Count > 0)
        {
            var TempPath = path.Dequeue();
            var TempNode = folders.Dequeue();
            var x = SearchFolders(TempNode, TempPath);
            if (x == null) { throw new Exception(); }
            folders.Enqueue(x);
            result = x;
        }
        return result;

    }

    protected Folder SearchFolders(IFolder node, string path)
    {
        foreach (var item in node.GetFolders())
        {
            if (item.Name == path) return (Folder)item;
        }
        return null;
    }

    protected File SearchFiles(IFolder node, string path)
    {
        foreach (var item in node.GetFiles())
        {
            if (item.Name == path) return (File)item;
        }
        return null;
    }
    protected List<string> Path(string originalPath)
    {

        var x = originalPath.Split("/", StringSplitOptions.RemoveEmptyEntries);

        return x.ToList<string>();
    }

    public IFileSystem GetRoot(string path)
    {
        var folder = GetFOlderSInInterfaz(path);
        if (folder == null) { throw new Exception("La carpeta no ha sido encontrada"); }

        return new FileSystem(folder);
    }

    public void Move(string origin, string destination)
    {
        this.Copy(origin, destination);
        this.Delete(origin);
    }
}

#endregion


#region Recorridos


public static class Recorrer
{
    public static IEnumerable<Folder> PreOrden(Folder folder)
    {
        yield return folder;

        foreach (var child in folder.GetFoldersSinInterfaz())
        {
            foreach (var item in PreOrden(child))
            {
                yield return item;
            }
        }
    }


    public static IEnumerable<Folder> BFS(Folder folder)
    {
        Queue<Folder> cola = new Queue<Folder>();
        cola.Enqueue(folder);

        while (cola.Count > 0)
        {
            var x = cola.Dequeue();
            yield return x;

            foreach (var item in x.GetFoldersSinInterfaz())
            {
                cola.Enqueue(item);
            }
        }
    }

}

#endregion



public static class Blended
{
    public static Folder Blendeed(Folder padre, Folder Salida)  // Mezclar nuevo y viejo para conseguri las combinaciones
    // Presenta una resucrsividad  directa e indirecta entre los metodos dependientes
    {

        var child = AddFolders(Salida);
        for (int k = 0; k < padre.childrenFolders.Count; k++)
        {
            var item = padre.childrenFolders[k];
            if (item.Name == child.Name)
            {
                item.Remove();
                var temp = Mezclar(item, child);
                temp.Father = padre;
                var y = AddFolders(temp);
                padre.AddChild(y);
                return padre;
            }
        }
        return padre;
    }

    private static Folder Mezclar(Folder a, Folder b)//Mezclar las carpetas
    {

        for (int j = 0; j < b.childrenFolders.Count; j++)   //Mezclar los hijos
        {

            var item = b.childrenFolders[j];
            bool x = true;
            for (int i = 0; i < a.childrenFolders.Count; i++)
            {
                var child = a.childrenFolders[i];
                if (child.Name == item.Name)
                {

                    x = false;
                    var m = item;
                    m = Mezclar(item, child);
                    a.childrenFolders[i] = AddFolders(m);
                }
            }
            if (x)
            {
                var copy = AddFolders(item);
                a.childrenFolders.Add(copy);
            }

        }

        for (int j = 0; j < b.files.Count; j++)
        {
            var file = b.files[j];
            bool x = true;

            for (int i = 0; i < a.files.Count; i++)
            {
                var temp = a.files[i];
                if (file.Name == temp.Name)
                {
                    x = false;
                    temp.ChangeFileValue(file.Size);
                }
            }
            if (x)
            {
                a.CreateFile(file.Name, file.Size);
            }
        }

        return a;
    }

    public static Folder AddFolders(Folder child)//Recursivamente generar un clon del nuevo arbol con valores de referencia distintos
    //Puede darse los grupos de casos que la referencia pueda desequilibrar el sistema por eliminar en una rama que antes fue copiada

    {
        var temp = new Folder(child.Name);
        // temp.addFather(child.Father); quitar por referencia 
        foreach (var item in child.files) { temp.CreateFile(item.Name, item.Size); }
        foreach (var item in child.childrenFolders) { var x = AddFolders(item); temp.AddChild(x); }
        return temp;
    }


}




