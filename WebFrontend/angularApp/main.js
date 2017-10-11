import './styles.scss';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { HcModule } from './hc.module';
if (module.hot) {
    module.hot.accept();
}
platformBrowserDynamic().bootstrapModule(HcModule);
//# sourceMappingURL=main.js.map