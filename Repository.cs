using System;
using System.Runtime.InteropServices;
using Libgit2.Internals;

namespace Libgit2
{
    public unsafe sealed partial class Repository : Libgit2Object
    {
        internal Repository(git_repository* nativeHandle, bool handleOwner)
            : base(nativeHandle, handleOwner)
        {
            Assert.NotNull(nativeHandle);

            NativeHandle = nativeHandle;
        }

        public Reference Head
        {
            get
            {
                Reference head = null;

                using (libgit2.Lock())
                {
                    git_reference* reference;
                    if (NativeMethods.git_repository_head(&reference, NativeHandle))
                    {
                        head = new Reference(reference);
                    }
                }

                return head;
            }
        }
        public bool HeadIsDetached
        {
            get { using (libgit2.Lock()) return NativeMethods.git_repository_head_detached(NativeHandle); }
        }
        public bool HeadIsUnborn
        {
            get { using (libgit2.Lock()) return NativeMethods.git_repository_head_unborn(NativeHandle); }
        }
        public bool IsBare
        {
            get { using (libgit2.Lock()) return NativeMethods.git_repository_is_bare(NativeHandle); }
        }
        public bool IsEmpty
        {
            get { using (libgit2.Lock()) return NativeMethods.git_repository_is_empty(NativeHandle); }
        }
        public bool IsShallow
        {
            get { using (libgit2.Lock()) return NativeMethods.git_repository_is_shallow(NativeHandle); }
        }
        public mstring Message
        {
            get
            {
                mstring message = null;

                lock (libgit2.Lock())
                {
                    git_buf buf = new git_buf();
                    if (NativeMethods.git_repository_message(&buf, NativeHandle))
                    {
                        message = new mstring(buf.ptr, (int)buf.size);
                    }
                }

                return message;
            }
        }
        public mstring Namespace
        {
            get { using (libgit2.Lock()) return (mstring)NativeMethods.git_repository_get_namespace(NativeHandle); }
        }
        public mstring Path
        {
            get { using (libgit2.Lock()) return (mstring)NativeMethods.git_repository_path(NativeHandle); }
        }
        public RepositoryState State
        {
            get { using (libgit2.Lock()) return NativeMethods.git_repository_state(NativeHandle); }
        }
        public mstring WorkingDirectory
        {
            get { using (libgit2.Lock()) return (mstring)NativeMethods.git_repository_workdir(NativeHandle); }
        }

        public Checkout.NotificationCallback CheckoutNotification;
        public Checkout.ProgressCallback CheckoutProgres;
        public Checkout.PerformanceCallback CheckoutPerformance;
        public Merge.HeadCallback MergeHead;

        internal readonly git_repository* NativeHandle;

        public void ClearMessage()
        {
            result result;
            lock (libgit2.Lock())
            {
                result = NativeMethods.git_repository_message_remove(NativeHandle);
            }
            Assert.Success(result);
        }

        public static bool Clone(mstring url, mstring path, CloneOptions options, out Repository repository)
        {
            Ensure.NotNull(url, nameof(url));
            Ensure.NotNull(path, nameof(path));

            repository = null;

            git_repository* repo;
            git_checkout_options checkout_opts;
            git_clone_options clone_opts;
            git_fetch_options fetch_opts;
            git_remote_callbacks remote_callbacks;

            options.ToNative(out clone_opts, out checkout_opts, out fetch_opts, out remote_callbacks);

            fetch_opts.callbacks = &remote_callbacks;

            using (libgit2.Lock())
            {
                if (NativeMethods.git_clone(&repo, url, path, &clone_opts))
                {
                    repository = new Repository(repo, true);
                }
            }

            return repository != null;
        }

        public static bool Create(ObjectDatabase objectDatabase, ReferenceDatabase referenceDatabase, out Repository respository)
        {
            Ensure.NotNull(objectDatabase, nameof(objectDatabase));
            Ensure.NotNull(referenceDatabase, nameof(referenceDatabase));

            respository = null;

            git_repository* repo;
            git_odb* odb = objectDatabase.NativeHandle;
            git_refdb* refdb = referenceDatabase.NativeHandle;

            if (NativeMethods.git_repository_new(&repo))
            {
                NativeMethods.git_repository_set_odb(repo, odb);
                NativeMethods.git_repository_set_refdb(repo, refdb);

                respository = new Repository(repo, true);
            }

            return respository != null;
        }

        public bool DetachHead()
        {
            using (libgit2.Lock())
            {
                return NativeMethods.git_repository_detach_head(NativeHandle);
            }
        }

        public static bool Discover(mstring path, out mstring repositoryPath)
        {
            Ensure.NotNull(path, nameof(path));

            repositoryPath = null;

            git_buf buf;

            using (libgit2.Lock())
            {
                if (NativeMethods.git_repository_discover(&buf, path, false, null))
                {
                    repositoryPath = new mstring(buf.ptr, (int)buf.size);
                }
            }

            return repositoryPath != null;
        }

        public bool GetIdentity(out mstring name, out mstring email)
        {
            name = null;
            email = null;

            using (libgit2.Lock())
            {
                byte* pName;
                byte* pEmail;

                if (NativeMethods.git_repository_ident(&pName, &pEmail, NativeHandle))
                {
                    name = new mstring(pName);
                    email = new mstring(email);
                }
            }

            return name != null
                || email != null;
        }

        public static bool Init(mstring path, bool makeBare, out Repository repository)
        {
            Ensure.NotNull(path, nameof(path));

            repository = null;

            git_repository* repo;

            using (libgit2.Lock())
            {
                if (NativeMethods.git_repository_init(&repo, path, makeBare))
                {
                    Assert.NotNull(repo);

                    repository = new Repository(repo, true);
                }
            }

            return repository != null;
        }

        public static bool Init(mstring path, RepositoryOptions options, out Repository repository)
        {
            Ensure.NotNull(path, nameof(path));

            repository = null;

            git_repository* repo;
            git_repository_init_options opts = options;

            using (libgit2.Lock())
            {
                if (NativeMethods.git_repository_init_ext(&repo, path, &opts))
                {
                    Assert.NotNull(repo);

                    repository = new Repository(repo, true);
                }
            }

            return repository != null;
        }

        public static bool Open(mstring path, out Repository repository)
        {
            Ensure.NotNull(path, nameof(path));

            repository = null;

            git_repository* repo;

            using (libgit2.Lock())
            {
                if (NativeMethods.git_repository_open(&repo, path))
                {
                    Assert.NotNull(repo);

                    repository = new Repository(repo, true);
                }
            }

            return repository != null;
        }

        public static bool Open(mstring path, RepositoryFlags flags, out Repository repository)
        {
            Ensure.NotNull(path, nameof(path));

            repository = null;

            git_repository* repo;

            using (libgit2.Lock())
            {
                if (NativeMethods.git_repository_open_ext(&repo, path, flags, null))
                {
                    Assert.NotNull(repo);

                    repository = new Repository(repo, true);
                }
            }

            return repository != null;
        }

        public static bool OpenBare(mstring path, out Repository repository)
        {
            Ensure.NotNull(path, nameof(path));

            repository = null;

            git_repository* repo;

            using (libgit2.Lock())
            {
                if (NativeMethods.git_repository_open_bare(&repo, path))
                {
                    Assert.NotNull(repo);

                    repository = new Repository(repo, true);
                }
            }

            return repository != null;
        }

        public bool SetHead(mstring commitish)
        {
            if (commitish == null)
                return false;

            using (libgit2.Lock())
            {
                return NativeMethods.git_repository_set_head(NativeHandle, commitish);
            }
        }

        public bool SetHead(Oid objectId)
        {
            if (objectId == null)
                return false;

            using (libgit2.Lock())
            {
                git_oid oid = objectId._oid;
                return NativeMethods.git_repository_set_head_detached(NativeHandle, &oid);
            }
        }

        public bool SetIdentity(mstring name, mstring email)
        {
            using (libgit2.Lock())
            {
                return NativeMethods.git_repository_set_ident(NativeHandle, name, email);
            }
        }

        public bool SetNamespace(mstring @namespace)
        {
            if (@namespace == null)
                return false;

            using (libgit2.Lock())
            {
                return NativeMethods.git_repository_set_namespace(NativeHandle, @namespace);
            }
        }

        public bool SetWorkingDirectory(mstring workingDirectory, bool updateGitlink)
        {
            if (workingDirectory == null)
                return false;

            using (libgit2.Lock())
            {
                return NativeMethods.git_repository_set_workdir(NativeHandle, workingDirectory, updateGitlink);
            }
        }

        public bool StateCleanup()
        {
            using (libgit2.Lock())
            {
                return NativeMethods.git_repository_state_cleanup(NativeHandle);
            }
        }

        protected internal override void Free()
        {
            NativeMethods.git_repository_free(NativeHandle);
        }

        private result checkout_notification_callback(CheckoutNotifyFlags why, byte* path, git_diff_file* baseline, git_diff_file* target, git_diff_file* workdir, void* payload)
        {
            if (CheckoutNotification == null)
                return ErrorCode.Ok;

            return CheckoutNotification(why, (mstring)path, new DiffFile(baseline), new DiffFile(target), new DiffFile(workdir));
        }

        private void checkout_perfdata_cb(git_checkout_perfdata* data, void* payload)
        {
            CheckoutPerformance(new CheckoutPerformanceData(data));
        }

        private result checkout_progress_cb(byte* path, UIntPtr completedSteps, UIntPtr totalSteps, void* payload)
        {
            throw new NotImplementedException();
        }

        internal unsafe result merge_head_foreach_cb(git_oid* oid, void* payload)
        {
            throw new NotImplementedException();
        }
    }

    public partial class Repository
    {
        public static CreateCallback RepositoryCreate;

        internal static unsafe result create_cb(git_repository** repository, byte* path, bool bare, void* payload)
        {
            if (RepositoryCreate == null)
                return ErrorCode.Error;

            result result;

            Repository repo;
            if( result = RepositoryCreate(out repo, (mstring)path, bare))
            {
                *repository = repo.NativeHandle;
            }

            return result;
        }

        public delegate ErrorCode CreateCallback(out Repository repository, mstring path, bool bare);
    }
}
