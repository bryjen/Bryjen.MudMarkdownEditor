import {common, createStarryNight} from '@wooorm/starry-night'
import {toHtml} from 'hast-util-to-html'
import markdownIt from 'markdown-it'

window.getCssStyles = function () {
    return "another test css";
}

window.parseMarkdown = async function (rawHtml) {
    const starryNight = await createStarryNight(common)
    const tree = starryNight.highlight(rawHtml, 'source.cs')
    const asHtml = toHtml(tree)

    console.log(asHtml)
    return asHtml;
}

window.parseMarkdownWithIt = async function (rawMarkdown) {
    const starryNight = await createStarryNight(common)

    const markdownItInstance = markdownIt({
        highlight(value, lang) {
            const scope = starryNight.flagToScope(lang)

            return toHtml({
                type: 'element',
                tagName: 'pre',
                properties: {
                    className: scope
                        ? [
                            'highlight',
                            'highlight-' + scope.replace(/^source\./, '').replace(/\./g, '-')
                        ]
                        : undefined
                },
                children: scope
                    ? /** @type {Array<ElementContent>} */ (
                        starryNight.highlight(value, scope).children
                    )
                    : [{type: 'text', value}]
            })
        }
    })
    
    return markdownItInstance.render(rawMarkdown);
}
