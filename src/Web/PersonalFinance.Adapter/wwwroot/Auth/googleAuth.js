window.googleAuth = {
    dotNetRef: null,

    renderButton: async function (clientId, containerId, dotNetRef, locale) {
        await window.googleAuth.waitForScript();

        window.googleAuth.dotNetRef = dotNetRef;

        google.accounts.id.initialize({
            client_id: clientId,
            callback: window.googleAuth.handleCredential
        });

        google.accounts.id.renderButton(document.getElementById(containerId), {
            type: "standard",
            theme: "outline",
            size: "large",
            shape: "pill",
            text: "signin_with",
            logo_alignment: "center",
            width: 400,
            locale: locale
        });
    },

    handleCredential: function (response) {
        window.googleAuth.dotNetRef?.invokeMethodAsync("OnGoogleCredential", response.credential);
    },

    waitForScript: function () {
        return new Promise((resolve, reject) => {
            if (window.google?.accounts?.id) {
                resolve();
                return;
            }

            let attempts = 0;

            const interval = setInterval(() => {
                if (window.google?.accounts?.id) {
                    clearInterval(interval);
                    resolve();
                } else if (++attempts > 100) {
                    clearInterval(interval);
                    reject(new Error("Google Identity Services did not load."));
                }
            }, 50);
        });
    }
};
