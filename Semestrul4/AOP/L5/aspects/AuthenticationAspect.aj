package aspects;

import java.util.HashMap;
import java.util.Map;

import javax.swing.JOptionPane;

public aspect AuthenticationAspect
{
    public AuthenticationAspect()
    {
        auth = new HashMap<String, String>();
        auth.put("Admin", "1234");
    }
    
    private boolean Authenticate()
    {
        String userName = JOptionPane.showInputDialog("Username:");
        String password = JOptionPane.showInputDialog("Password");
        return password.equals(auth.get(userName));
    }
    
    private Map<String,String> auth;
    
    pointcut changes() : call(@ChangesState * aop5.domain.MarksApplication.*(..));
    
    Object around() : changes()
    {
        if (Authenticate())
            return proceed();
        else
        {
            JOptionPane.showMessageDialog(null, "Invalid credentials! Access Denied!", "Error!",
                            JOptionPane.ERROR_MESSAGE);
            throw new RuntimeException("Invalid credentials! Access Denied!");
        }
    }
}
