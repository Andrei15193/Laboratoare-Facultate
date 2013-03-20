package aop1;

import javax.swing.SwingUtilities;

import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import org.apache.log4j.PropertyConfigurator;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import aop1.presentation.ApplicationLauncherFrame;

public final class Application
{
    public Application(final ApplicationLauncherFrame applicationLauncherFrame)
    {
        Application.logger.log(Level.INFO, "Application.Application("
                        + applicationLauncherFrame.toString()
                        + " ApplicationLauncherFrame)");
        this.applicationLauncherFrame = applicationLauncherFrame;
    }

    public void start()
    {
        Application.logger.log(Level.INFO, "Application.start()");
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
        Application.logger.log(Level.INFO,
                        "Application.main(" + args.toString() + " :String[])");
        PropertyConfigurator.configure("src/log4j.properties");
        ApplicationContext factory = new ClassPathXmlApplicationContext(
                        "config.xml");
        Application application = (Application)factory.getBean("application");
        application.start();
    }

    public final static Logger logger = Logger.getLogger("aop1");
    private final ApplicationLauncherFrame applicationLauncherFrame;
}
