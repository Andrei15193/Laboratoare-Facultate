package aop2;

import java.io.FileInputStream;
import java.io.IOException;
import java.util.logging.LogManager;
import java.util.logging.Logger;

import javax.swing.SwingUtilities;

import org.apache.log4j.PropertyConfigurator;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import aop2.presentation.ApplicationLauncherFrame;

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

    public static void main(String[] args) throws SecurityException,
                    IOException
    {
        Application.setUpLoggers();
        ApplicationContext factory = new ClassPathXmlApplicationContext(
                        "config.xml");
        Application application = (Application)factory.getBean("application");
        application.start();
    }

    private static void setUpLoggers() throws SecurityException, IOException
    {
        FileInputStream file = new FileInputStream("src/logging.properties");
        LogManager.getLogManager().readConfiguration(file);
        file.close();
        Application.logger = Logger.getLogger("aop2logger");
        PropertyConfigurator.configure("src/log4j.properties");
        Application.log4j = org.apache.log4j.Logger.getLogger("aop2log4j");
    }

    public static Logger logger;
    public static org.apache.log4j.Logger log4j;
    private final ApplicationLauncherFrame applicationLauncherFrame;
}
