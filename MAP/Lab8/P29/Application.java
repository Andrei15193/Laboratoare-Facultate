import javax.swing.SwingUtilities;

import repository.BankRepository;
import repository.DepositRepository;
import repository.PersonRepository;
import repository.RepositoryException;
import repository.file.BankRepositoryFile;
import repository.file.DepositRepositoryFile;
import repository.file.PersonRepositoryFile;
import userInterface.MainWindow;
import controller.Controller;
import domain.Deposit;

public class Application
{
    public static void main(String[] args)
    {
        try
        {
            final BankRepository bankRepo = new BankRepositoryFile("banks");
            final DepositRepository depositRepo = new DepositRepositoryFile(
                            "deposits");
            final PersonRepository personRepo = new PersonRepositoryFile(
                            "persons");
            depositRepo.add(new Deposit("ING", "0960229123456", 1000, true));
            final Controller controller = new Controller(bankRepo, depositRepo,
                            personRepo);
            SwingUtilities.invokeLater(new Runnable()
            {
                @Override
                public void run()
                {
                    MainWindow mainWindow = new MainWindow(controller);
                    mainWindow.setVisible(true);
                }
            });
        }
        catch (RepositoryException e)
        {
            e.printStackTrace();
        }
    }
}
