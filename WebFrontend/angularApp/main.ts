import './styles.scss'
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { HcModule } from './hc.module';


// Entry point for JiT compilation.
declare var System: any;

// Styles.
// Enables Hot Module Replacement.
declare var module: any;
if (module.hot) {
    module.hot.accept();
}

platformBrowserDynamic().bootstrapModule(HcModule);
