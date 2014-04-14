package ro.andrei15193.bitbucketbrowser;

import ro.andrei15193.bitbucketbrowser.model.Repository;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.widget.TextView;

public class RepositoryActivity extends ActionBarActivity
{
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		Repository repository = _getApplication().getRepository();

		setContentView(R.layout.activity_repository);
		setTitle(repository.getName() + " Details");

		((TextView)findViewById(R.id.repository_owner)).setText(" " + repository.getOwner());
		((TextView)findViewById(R.id.repository_language)).setText(" " + repository.getLanguage());
		((TextView)findViewById(R.id.repository_description)).setText(repository.getDescription());
	}

	private final BitBucketBrowserApplication _getApplication()
	{
		return (BitBucketBrowserApplication)getApplication();
	}
}