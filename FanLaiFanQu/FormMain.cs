using GoogleTranslateFreeApi;

// https://github.com/wadereye/GoogleTranslateFreeApi
namespace FanLaiFanQu {
    public partial class FormMain : Form {
        private readonly SynchronizationContext? context;
        public FormMain() {
            context = SynchronizationContext.Current;
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e) {
        }

        private void btnTrans_Click(object sender, EventArgs e) {
            string originalText = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(originalText)) {
                MessageBox.Show("请输入文本内容");
                return;
            }
            this.btnTrans.Enabled = false;
            new Thread(new ThreadStart(async () => {
                var translator = new GoogleTranslator();
                TranslationResult res1 = await translator.TranslateAsync(originalText, Language.ChineseSimplified, Language.English);
                res1 = await translator.TranslateAsync(res1.MergedTranslation, Language.English, Language.ChineseSimplified);
                TranslationResult res2 = await translator.TranslateAsync(res1.MergedTranslation, Language.ChineseSimplified, Language.Spanish);
                res2 = await translator.TranslateAsync(res2.MergedTranslation, Language.Spanish, Language.ChineseSimplified);
                context?.Post((o) => {
                    txtOutput.Text = res1.MergedTranslation + "\r\n\r\n\r\n\r\n" + res2.MergedTranslation;
                    this.btnTrans.Enabled = true;
                }, null);
            })).Start();
        }
    }
}