namespace filesystem
{

    public delegate bool FileFilter(IFile file);

    public interface IFile //Archivo
    {
        int Size { get; }
        string Name { get; }
    }

    public interface IFolder //carpeta
    {
        /// <summary>
        ///  La definición de carpeta también tiene un nombre, y algunos métodos adcionales
        //que usted debe implementar que veremos a continuación:

        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        string Name { get; }
        /// <summary>
        ///  Este método devuelve una instancia de IFile que representa el archivo recién creado, y que por supuesto debe coincidir en nombre y tamaño con los argumentos pasados.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        IFile CreateFile(string name, int size);

        /// <summary>
        ///  Otro método similar es CreateFolder que crea una subcarpeta dentro de la carpeta correspondiente
        /// </summary>
        /// <param name="">Al intentar crear una carpeta o archivo que ya existe, usted debe lanzar una excepción.</param>
        /// <returns></returns>

        IFolder CreateFolder(string name);
        /// <summary>
        ///  Al intentar crear una carpeta o archivo que ya existe, usted debe lanzar unaexcepción.

        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        IEnumerable<IFile> GetFiles();
        /// <summary>
        ///  Al intentar crear una carpeta o archivo que ya existe, usted debe lanzar una excepción.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        IEnumerable<IFolder> GetFolders();
        /// <summary>
        ///  el método TotalSize devuelve el tamaño total de todos los archivos contenidos en la carpeta correspondiente y todas sus subcarpetas, recursivamente. Se asume que las carpetas tienen tamaño 0.

        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        int TotalSize();
    }

    public interface IFileSystem
    {
        /// <summary>
        ///  evuelven respectivamente una carpeta o archivo según su dirección.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        IFolder GetFolder(string path);
        /// <summary>
        ///  evuelven respectivamente una carpeta o archivo según su dirección.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        IFile GetFile(string path);
        /// <summary>
        ///  uenta con el método GetRoot que devuelve un IFileSystem centrado en la carpeta especificada.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        IFileSystem GetRoot(string path);
        /// <summary>
        ///  el método Find enumera todos los archivos que cumplen con dicho predicado, en preorden, (primero los archivos de la carpeta actual y luego recursivamente los archivos de las subcarpetas) y recorriendo los archivos y carpetas en orden alfabético.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        IEnumerable<IFile> Find(FileFilter filter);

        void Copy(string origin, string destination);
        void Move(string origin, string destination);
        void Delete(string path);
    }
}
