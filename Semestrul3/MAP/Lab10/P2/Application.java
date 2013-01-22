import javax.swing.SwingUtilities;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import presentation.ApplicationLauncherFrame;

public class Application
{
    public Application(final ApplicationLauncherFrame applicationLauncherFrame)
    {
        this.applicationLauncherFrame = applicationLauncherFrame;
    }

    public void start()
    {
        SwingUtilities.invokeLater(new Runnable()
        {
            @Override
            public void run()
            {
                Application.this.applicationLauncherFrame.setVisible(true);
            }
        });
    }

    public static void main(String[] args)
    {
        ApplicationContext factory = new ClassPathXmlApplicationContext(
                        "config.XML");
        Application application = (Application)factory.getBean("application");
        application.start();
    }

    private final ApplicationLauncherFrame applicationLauncherFrame;
}
