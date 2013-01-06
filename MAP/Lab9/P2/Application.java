import javax.swing.SwingUtilities;

import persistence.AllCourses;
import persistence.AllMarks;
import persistence.AllStudents;
import persistence.file.AllCoursesInFile;
import persistence.file.AllMarksInFile;
import persistence.file.AllStudentsInFile;
import presentation.ApplicationLauncherFrame;

public class Application
{
    public static void main(String[] args)
    {
        final AllCourses allCourses = new AllCoursesInFile("courses");
        final AllMarks allMarks = new AllMarksInFile("marks");
        final AllStudents allStudents = new AllStudentsInFile("students");
        SwingUtilities.invokeLater(new Runnable()
        {
            @Override
            public void run()
            {
                new ApplicationLauncherFrame(allCourses, allMarks, allStudents)
                                .setVisible(true);
            }
        });
    }
}
