import javax.swing.SwingUtilities;

import persistence.file.AllCoursesInFile;
import persistence.file.AllMarksInFile;
import persistence.file.AllStudentsInFile;
import presentation.ApplicationLauncherFrame;
import controller.LauncherController;
import domain.MarksApplication;

public class Application
{
    public static void main(String[] args)
    {
        SwingUtilities.invokeLater(new Runnable()
        {
            @Override
            public void run()
            {
                new ApplicationLauncherFrame(
                                new LauncherController(new MarksApplication(
                                                new AllStudentsInFile(
                                                                "allStudents"),
                                                new AllCoursesInFile(
                                                                "allCourses"),
                                                new AllMarksInFile("allMarks"))))
                                .setVisible(true);
            }
        });
    }
}
