using System.Diagnostics;
using filesystem;

class Program
{
    static void Main()
    {

        // Esto está aquí para que no te olvides de implementarlo
        Console.WriteLine($"{Exam.Nombre} - {Exam.Grupo}");

        // Creando un sistema de ficheros vacío
        var fs = Exam.CreateFileSystem();

        // Creando un par de carpetas en la raíz
        var root = fs.GetFolder("/");

        var home = root.CreateFolder("home");
        var tmp = root.CreateFolder("tmp");

        // Creando 10 archivos dentro de la carpeta `tmp`
        for (int i = 0; i < 10; i++)
            tmp.CreateFile($"file{i}.tmp", 10);

        // Verificando el tamaño de `tmp`
        Debug.Assert(tmp.TotalSize() == 100);

        // Creando archivos en `home`
        home.CreateFile("picture.png", 20);
        home.CreateFile("document.docx", 150);
        home.CreateFile("virus.exe", 300);

        // Buscando un archivo concreto
        var virusFile = fs.GetFile("/home/virus.exe");
        Debug.Assert(virusFile.Name == "virus.exe");

        // Verificando el método `Find` con archivos grandes
        foreach (var file in fs.Find(file => file.Size > 50))
            Debug.Assert(file.Size > 50);

        // Verificando el método `Find` con nombres
        foreach (var file in fs.Find(file => file.Name.EndsWith(".png")))
            Debug.Assert(file.Name == "picture.png");

        // Ahora vamos a copiar `/tmp` para `/home` y verificar los tamaños
        fs.Copy("/tmp", "/home");
        Debug.Assert(home.TotalSize() == 570);
        Debug.Assert(fs.GetFolder("/tmp").TotalSize() ==
                     fs.GetFolder("/home/tmp").TotalSize());

        // Añade tus pruebas aquí
        // ...


        var x = Exam.CreateFileSystem();
        var rootFolder = x.GetFolder("/");
        var uno = rootFolder.CreateFolder("uno");

        var seis = uno.CreateFolder("seis");
        seis.CreateFile("F6", 10);

        var siete = uno.CreateFolder("siete");
        siete.CreateFile("F7", 7);
        uno.CreateFile("F1", 10);
        uno.CreateFile("F11", 5);
        var dos = rootFolder.CreateFolder("dos");

        var UnoCopy = dos.CreateFolder("uno");
        UnoCopy.CreateFolder("seis").CreateFolder("Ocho");
        var tres = dos.CreateFolder("tres");
        tres.CreateFile("F3", 2);

        var cuatro = dos.CreateFolder("cuatro");
        cuatro.CreateFile("F4", 3);
        cuatro.CreateFile("F44", 3);
        var cinco = cuatro.CreateFolder("cinco");
        cinco.CreateFile("F5", 5);


        System.Console.WriteLine(rootFolder.TotalSize());
        x.Copy("/uno", "/dos");

        x.Delete("/uno");
        x.Delete("/dos/uno/seis");
        //x.Delete("/dos/uno/siete");
        var simete = x.GetFolder("/dos/uno/siete");
        x.Move("dos/uno/siete/F7", "/dos");






        Test2();

    }

    static void CrearFile(IFolder folder, int cant)
    {
        for (int i = 1; i <= cant; i++)
        {
            folder.CreateFile(folder.Name + i, i * 10);
        }
    }

    static void Test1()
    {

        var nuevo = Exam.CreateFileSystem();

        var nuevoRoot = nuevo.GetFolder("/");
        CrearFile(nuevoRoot, 3);

        var RootChild2 = nuevoRoot.CreateFolder("2");
        CrearFile(RootChild2, 3);

        var tresChild = RootChild2.CreateFolder("3");
        CrearFile(tresChild, 2);
        var cuatroChild = tresChild.CreateFolder("4");
        CrearFile(cuatroChild, 1);
        var cincoChild = tresChild.CreateFolder("5");
        CrearFile(cincoChild, 5);

        var SeisChild = RootChild2.CreateFolder("6");
        CrearFile(SeisChild, 5);
        var SieteChild = SeisChild.CreateFolder("7");
        CrearFile(SieteChild, 5);
        var OchoChild = SieteChild.CreateFolder("8");
        CrearFile(OchoChild, 1);


        var RootChild9 = nuevoRoot.CreateFolder("9");



        var RootChild10 = nuevoRoot.CreateFolder("10");
        var Once = RootChild10.CreateFolder("11");
        var doce = RootChild10.CreateFolder("12");
        CrearFile(doce, 5);
        var trece = RootChild10.CreateFolder("13");
        CrearFile(trece, 3);

        var nnnn = nuevoRoot.TotalSize();
        System.Console.WriteLine(nnnn);
    }

    static void Test2()
    {
        var System = Exam.CreateFileSystem();
        var root = System.GetFolder("/");
        var one = root.CreateFolder("1");
        one.CreateFile("uno", 1);
        one.CreateFile("e", 1);
        var three = one.CreateFolder("3");
        three.CreateFile("yy", 55);
        var four = one.CreateFolder("4");


        var two = root.CreateFolder("2");

        var five = two.CreateFolder("5");
        var one1 = five.CreateFolder("1");
        one1.CreateFile("uno", 5);
        one1.CreateFile("w", 4);
        var z = one1.CreateFolder("3");
        z.CreateFile("yy", 55);
        z.CreateFile("aa", 88);


        var nine = one1.CreateFolder("9");
        var ten = nine.CreateFolder("10");

        var six = two.CreateFolder("6");

        var one6 = six.CreateFolder("1");
        var three6 = one6.CreateFolder("3");
        var four6 = one6.CreateFolder("4");


        System.Copy("/1", "/2/5");
        var t = System.GetFolder("/2/5/1");
        // t.CreateFile("hola", 20);
        Console.WriteLine(6);



    }
}
