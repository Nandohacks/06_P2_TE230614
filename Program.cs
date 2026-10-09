//Ejercicio 6

namespace InspeccionCeldaApp
{
    // CLASE: Modela la estación de inspección y gestiona el conteo y calidad del lote
    public class CeldaInspeccion
    {
        // PROPIEDADES
        public int TotalPiezas { get; private set; }
        public int PiezasCorrectas { get; private set; }
        public int PiezasDefectuosas { get; private set; }

        // CONSTRUCTOR: Inicializa la celda con el tamaño fijado para el lote
        public CeldaInspeccion(int totalPiezas = 10)
        {
            TotalPiezas = totalPiezas;
            PiezasCorrectas = 0;
            PiezasDefectuosas = 0;
        }

        // MÉTODO: Procesa la evaluación de una pieza individual y actualiza contadores
        public bool RegistrarPieza(int estado)
        {
            if (estado == 1)
            {
                PiezasCorrectas++;
                return true;
            }
            else if (estado == 0)
            {
                PiezasDefectuosas++;
                return true;
            }
            return false; // Retorna falso si el valor ingresado no es 1 o 0
        }

        // MÉTODO: Calcula el porcentaje de calidad del lote evitando divisiones entre cero
        public double CalcularPorcentajeCorrectas()
        {
            if (TotalPiezas == 0) return 0.0; // Validacion contra division entre cero
            return ((double)PiezasCorrectas / TotalPiezas) * 100.0;
        }

        // MÉTODO: Evalúa si el lote cumple el estándar de calidad (mínimo 90%)
        public string DeterminarEstadoLote()
        {
            return CalcularPorcentajeCorrectas() >= 90.0 ? "ACEPTADO" : "RECHAZADO";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Inspección de Lote de Piezas - Ejercicio 06";

            const int TAMANO_LOTE = 10;

            // OBJETOS: Instanciación de la clase CeldaInspeccion
            CeldaInspeccion miCelda = new CeldaInspeccion(TAMANO_LOTE);

            Console.WriteLine("==================================================");
            Console.WriteLine("    CELDA AUTOMATIZADA DE INSPECCIÓN DE PIEZAS    ");
            Console.WriteLine("==================================================\n");
            Console.WriteLine($"Iniciando inspección de un lote de {TAMANO_LOTE} piezas.");
            Console.WriteLine("Instrucciones: Ingrese 1 para 'Pieza Correcta' o 0 para 'Pieza Defectuosa'.\n");

            // CICLO FOR: Iteración exacta para capturar las 10 piezas del lote
            for (int i = 1; i <= TAMANO_LOTE; i++)
            {
                int estado = LeerEstadoPiezaValido(i);
                miCelda.RegistrarPieza(estado);
            }

            // PROCESAMIENTO DE RESULTADOS FINALES
            double porcentajeValidas = miCelda.CalcularPorcentajeCorrectas();
            string resultadoLote = miCelda.DeterminarEstadoLote();

            // MOSTRAR RESULTADOS CON UNIDADES DE MEDIDA EXPLICITAS (piezas, %)
            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine("            INFORME DE CALIDAD DEL LOTE           ");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Total de piezas inspeccionadas: {miCelda.TotalPiezas} piezas");
            Console.WriteLine($"Piezas correctas:              {miCelda.PiezasCorrectas} piezas");
            Console.WriteLine($"Piezas defectuosas:            {miCelda.PiezasDefectuosas} piezas");
            Console.WriteLine($"Porcentaje de piezas válidas:  {porcentajeValidas:F2} %");

            // Resaltar estado del lote con color
            Console.Write("Estado final del lote:          ");
            if (resultadoLote == "ACEPTADO")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(resultadoLote);
            Console.ResetColor();
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("\nPresione cualquier tecla para finalizar el programa...");
            Console.ReadKey();
        }

        // MÉTODO AUXILIAR: Validación para impedir entradas distintas de 1 o 0 y evitar ciclos infinitos
        static int LeerEstadoPiezaValido(int numeroPieza)
        {
            int valor;
            bool esValido = false;

            do
            {
                Console.Write($"Pieza [{numeroPieza}/{10}] -> Estado: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out valor) && (valor == 0 || valor == 1))
                {
                    esValido = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" [ENTRADA RECHAZADA]: Valor no permitido. Ingrese únicamente 1 (Correcta) o 0 (Defectuosa).");
                    Console.ResetColor();
                }
            } while (!esValido); // Garantiza repetición hasta recibir una entrada correcta

            return valor;
        }
    }
}