function queryCheck() {
    const result = document.getElementById("result");
    const queryString = window.location.search;
    var ret = queryString + "<hr />";

    if (queryString > "") {
        const urlParams = new URLSearchParams(queryString);
        const entries = urlParams.entries();
        for (const entry of entries) {
            ret += `${entry[0]}: ${entry[1]} <br />`;
        }

        if (urlParams.has("hub.challenge")) {
            param = urlParams.get("hub.challenge");
            res = SendMail(param);
            console.log(res);
            ret = res;
        }

        result.innerHTML = ret;
    }
}

async function SendMail(Mensagem) {
    var Consulta = `https://whatsapp.larsoft.net/SendMail?Texto=${Mensagem}&ParaNome=Larsoft%20Developer&ParaEndereco=luciano%40larsoft.com.br&Assunto=Larsoft%20WhatsApp&DeNome=Larsoft%20Mailer`;
    var fetchPromise = await fetch(Consulta,);
    fetchPromise
        .then((response) => {
            if (!response.ok) {
                throw new Error(`HTTP error: ${response.status}`);
            }
            return response.json();
        })
        .then((data) => {
            console.log(data[0].name);
        })
        .catch((error) => {
            console.error(`Could not get products: ${error}`);
        });
}
