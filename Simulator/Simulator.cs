using BlApi;
using Bllmplementation;
using BO;
using System.Diagnostics;
namespace Simulator;
public static class Simulator
{
    private static string? previousState;
    private static string? afterState;
    static bool finishTheSimulator = false;
    public static event EventHandler StopSimulator;
    public static event EventHandler ProgressChange;
    public static void stoping()
    {
        finishTheSimulator = true;
        if (StopSimulator != null)
            StopSimulator("", EventArgs.Empty);
    }

    public static void run()
    {
        Thread mainThreads = new Thread(new ThreadStart(updatingChosenOrder));
        mainThreads.Start();
        return;
    }

    public static void updatingChosenOrder()
    {
        IBl bl = new Bllmplementation.Bl();
        int? id;
        while (!finishTheSimulator)
        {
            id = bl.Order.getOrderToPromote();
            if (id == null)
                stoping();
            else
            {
                try
                {
                    BO.Order currentOrder = bl.Order.GetOrderDetails((int)id);
                    previousState = currentOrder.Status.ToString();
                    Random rand = new Random();
                    int num = rand.Next(1000, 5000);
                    Details details = new Details(currentOrder, num);
                    if (ProgressChange != null)
                    {
                        ProgressChange(null, details);
                    }
                    Thread.Sleep(num);
                    afterState = (previousState == "Done" ? bl.Order.UpdateShipDate((int)id) : bl.Order.UpdateDeliveryDate((int)id)).Status.ToString();
                }
                catch (NegativeIdException u)
                {
                    return;

                }
            }
        }
        return;
    }
}
