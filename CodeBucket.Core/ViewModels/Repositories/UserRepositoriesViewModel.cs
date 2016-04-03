using CodeBucket.Core.ViewModels.Repositories;
using CodeBucket.Core.Services;
using ReactiveUI;
using BitbucketSharp;

namespace CodeBucket.Core.ViewModels.Repositories
{
    public class UserRepositoriesViewModel : RepositoriesViewModel, ILoadableViewModel
    {
        public string Username { get; private set; }

        public IReactiveCommand LoadCommand { get; }

        public UserRepositoriesViewModel(IApplicationService applicationService)
            : base(applicationService)
        {
            LoadCommand = ReactiveCommand.CreateAsyncTask(_ =>
            {
                Repositories.Items.Clear();
                return applicationService.Client.ForAllItems(x => x.Users.GetRepositories(Username), Repositories.Items.AddRange);
            });
        }

        public void Init(NavObject navObject)
        {
            Username = navObject.Username;
        }

        public class NavObject
        {
            public string Username { get; set; }
        }
    }
}